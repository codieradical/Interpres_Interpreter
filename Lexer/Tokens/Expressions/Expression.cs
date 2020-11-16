using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens.Expressions
{
    class Expression : AbstractExpression
    {
        public Expression(AbstractToken[] expressionTokens)
            : base(expressionTokens) { }

        public override string ToString()
        {
            return "(" + base.ToString() + ")";
        }
    }
}
