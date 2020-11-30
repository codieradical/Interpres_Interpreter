using Interpres.Expressions;
using Interpres.Tokens;
using Interpres.Tokens.Expressions;
using Interpres.Tokens.Logical;
using Interpres.Tokens.Numeracy;
using Interpres_dev;
using System.Collections.Generic;
using Expression = Interpres.Expressions.Expression;


class Parser
{
    private readonly AbstractToken[] _tokens;
    private int _pos;

    private List<string> _diag = new List<string>();

    public Parser(string text)
    {
        var tokenList = new List<AbstractToken>();

        var lexer = new LexerOld(text);
        AbstractToken abstractToken;

        do
        {
            abstractToken = lexer.NextToken();

            if (!(abstractToken is WhitespaceAbstractToken) &&
                !(abstractToken is InvalidAbstractToken))
            {
                tokenList.Add(abstractToken);
            }
        } while (!(abstractToken is EofAbstractToken));

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

        return new SyntaxTree(_diag, expression, typeof(EofAbstractToken));
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
