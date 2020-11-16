using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    class Assignment : AbstractOperator
    {
        public static Dictionary<string, ValueToken> variables = new Dictionary<string, ValueToken>
        {
            {"test", new ValueToken(10) }
        };

        public Assignment(int position) : base(position) { }

        public override string ToString()
        {
            return "=";
        }

        public override ValueToken Operate(params ValueToken[] values)
        {
            if (values.Length < 1)
                throw new InvalidOperationException("Not enough values provided.");

            object? result = null;

            if (values.Length > 2)
                throw new InvalidOperationException("Too many values.");

            if (!(values[0].Value is string))
                throw new InvalidOperationException("Invalid variable name");

            variables.Add((string)values[0].Value, values[1]);

            return new ValueToken(values[1]);
        }
    }
}
