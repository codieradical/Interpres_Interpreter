using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens.Logical
{
    class NotOperator : AbstractOperator
    {
        public NotOperator(int position) : base(position) { }

        public override string ToString()
        {
            return "!";
        }

        public override ValueToken Operate(params ValueToken[] values)
        {
            if (values.Length < 0)
                throw new InvalidOperationException("No value provided.");
            else if (values.Length > 1)
                throw new InvalidOperationException("Too many values provided.");

            if (values[0].IsBoolean())
            {
                dynamic l = values[0].Value;
                return new ValueToken(!l);
            }

            throw new InvalidOperationException($"Can't perform a not operation on a ${values[0].Value.GetType()}");
        }
    }
}
