using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens.Numeracy
{
    class PowerOperator : AbstractOperator
    {
        public PowerOperator(int position) : base(position) { }

        public override string ToString()
        {
            return "^";
        }

        public override ValueToken Operate(params ValueToken[] values)
        {
            if (values.Length < 1)
                throw new InvalidOperationException("Not enough values provided.");

            object? result = null;

            foreach (ValueToken value in values)
            {
                if (!value.IsNumeric())
                    throw new ArgumentException($"Token ${value} can't be multiplied.");

                if (result == null)
                {
                    result = value.Value;
                }
                else
                {
                    dynamic l = result;
                    dynamic r = value.Value;

                    result = Math.Pow(l, r);
                }
            }

            return new ValueToken(result);
        }
    }
}
