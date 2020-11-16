using Interpres.Lexer.Tokens;
using Interpres.Lexer.Tokens.Expressions;
using Interpres.Lexer.Tokens.Logical;
using Interpres.Lexer.Tokens.Numeracy;
using Interpres_dev;
using System.Collections.Generic;


class Parser
{
    private readonly AbstractToken[] _tokens;
    private int _pos;

    private List<string> _diag = new List<string>();

    public Parser(string text)
    {
        var tokenList = new List<AbstractToken>();

        var lexer = new Lexer(text);
        AbstractToken token;

        do
        {
            token = lexer.NextToken();

            if (!(token is WhitespaceToken) &&
                !(token is InvalidToken))
            {
                tokenList.Add(token);
            }
        } while (!(token is EOFToken));

        _tokens = tokenList.ToArray();

        // Report lexer issues too
        _diag.AddRange(lexer.Diagnostic);
    }

    public IEnumerable<string> Diagnostic => _diag;

    private AbstractToken Peek(int offset)
    {
        var index = _pos + offset;

        if (index >= _tokens.Length)
        {
            return _tokens[_tokens.Length - 1];
        }
        return _tokens[index];

    }

    private AbstractToken Current => Peek(0);

    private AbstractToken NextToken()
    {
        var current = Current;
        _pos++;
        return current;
    }

    private AbstractToken Match<T>()
    {
        if (Current.GetType() == typeof(T))
            return NextToken();

        _diag.Add($"ERROR: Unexpected Token <{Current.GetType()}>, expected <{typeof(T)}>");

        return null;
    }

    private AbstractToken ParseExpression()
    {
        return ParseTerm();
    }

    public SyntaxTree Parse()
    {
        var expression = ParseTerm();

        return new SyntaxTree(_diag, expression, typeof(EOFToken));
    }

    private AbstractToken ParseTerm()
    {
        var leftOperand = ParseFactor();

        while (Current.GetType() == typeof(AddOperator) ||
            Current.GetType() == typeof(SubtractOperator))
        {
            var operatorToken = NextToken();
            var rightOperand = ParseFactor();
            leftOperand = new BinaryExpression(leftOperand, operatorToken as AbstractOperator, rightOperand);
        }

        return leftOperand;
    }

    // Recursive parse tree
    private AbstractToken ParseFactor()
    {
        AbstractToken leftOperand = ParserPrimaryExpression();

        while (Current.GetType() == typeof(MultiplyOperator) ||
            Current.GetType() == typeof(DivideOperator))
        {
            var operatorToken = NextToken();
            var rightOperand = ParserPrimaryExpression();
            leftOperand = new BinaryExpression(leftOperand, operatorToken as AbstractOperator, rightOperand);
        }

        //leftOperand.Position = Current.Position;

        return leftOperand;
    }

    public AbstractToken ParserPrimaryExpression()
    {
        if (Current.GetType() == typeof(LeftParenthesisToken))
        {
            var expression = ParseExpression();

            return new Expression(new AbstractToken[] { expression});
        }
        var numberToken = Match<ValueToken>();

        return numberToken;
    }

}
