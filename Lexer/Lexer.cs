
using Interpres.Lexer.Tokens;
using Interpres_dev;
using System;
using System.Collections.Generic;

class Lexer
{
    private readonly string _text;
    private int _position;

    private List<string> _diag = new List<string>();
    public Lexer(string text)
    {
        this._text = text;
    }
    // Return diagnostics list.
    public IEnumerable<string> Diagnostic => _diag;

    private char Current
    {
        get
        {
            if (_position >= _text.Length)
                return '\0';

            return _text[_position];
        }
    }

    private void Next()
    {
        _position++;
    }

    public AbstractToken NextToken()
    {
        // <numbers>
        // <operators> + - * / %
        // <whitespaces>
        // <paranthesis>

        //EOF
        if (_position >= _text.Length)
        {
            return new EOFToken(_position);
        }

        //// Number tokens
        //if (char.IsDigit(Current))
        //{
        //    var start = _position;

        //    while (char.IsDigit(Current))
        //        Next();

        //    var length = _position - start;
        //    var text = _text.Substring(start, length);

        //    if (!int.TryParse(text, out var value))
        //    {
        //        _diag.Add($"ERROR: The number {_text} isn't a valid int32.");
        //    }

        //    return ValueToken.FromString(_position, text);
        //}

        // Whitespaces
        if (char.IsWhiteSpace(Current))
        {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);

            return new WhitespaceToken(start, text);
        }

        var tokenString = _text.Substring(_position);

        int end = tokenString.Length;

        for(int i = 0; i < end; i++)
        {
            if(char.IsWhiteSpace(tokenString[i]))
            {
                end = i;
                break;
            } else
            {
                Next();
            }
        }

        tokenString = tokenString.Substring(0, end);

        AbstractOperator abstractOperator = OperatorService.GetOperator(_position, tokenString);

        if (abstractOperator != null)
            return abstractOperator;

        if (Assignment.variables.ContainsKey(tokenString))
            return Assignment.variables[tokenString];

        try
        {
            return ValueToken.FromString(_position, tokenString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        _diag.Add($"ERROR: Bad character input: '{Current}'");
        return new InvalidToken(_position++, _text.Substring(_position - 1, 1));

    }


}
