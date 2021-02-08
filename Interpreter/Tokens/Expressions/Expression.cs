using Interpres.Tokens;
using Interpres.Tokens.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Expressions
{
    class Expression : Tokens.Expressions.Expression
    {
        public Expression(AbstractToken[] expressionTokens)
            : base(expressionTokens) { }

        public override string GetInputString()
        {
            return "(" + base.GetInputString() + ")";
        }
    }
}
