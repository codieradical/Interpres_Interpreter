using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens.Expressions
{
    class AbstractExpression : AbstractToken
    {
        public readonly AbstractToken[] expressionTokens;

        protected AbstractExpression(AbstractToken[] expressionTokens) {
            this.expressionTokens = expressionTokens;
        }

        public override string ToString()
        {
            return expressionTokens.ToString().Replace(",", " ").Replace("[", "").Replace("]", "");
        }
    }
}
