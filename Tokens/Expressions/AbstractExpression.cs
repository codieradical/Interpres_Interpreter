using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens.Expressions
{
    class Expression : AbstractToken
    {
        public readonly AbstractToken[] expressionTokens;

        protected Expression(AbstractToken[] expressionTokens) {
            this.expressionTokens = expressionTokens;
        }

        public override string GetInputString()
        {
            return expressionTokens.ToString().Replace(",", " ").Replace("[", "").Replace("]", "");
        }
    }
}
