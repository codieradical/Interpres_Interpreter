using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens.Expressions
{
    class BinaryExpression : AbstractExpression
    {
        public AbstractToken LeftOperand
        {
            get
            {
                return expressionTokens[0];
            }
        }

        public AbstractOperator Operation
        {
            get
            {
                return expressionTokens[1] as AbstractOperator;
            }
        }

        public AbstractToken RightOperand
        {
            get
            {
                return expressionTokens[2];
            }
        }

        public BinaryExpression(AbstractToken left, AbstractOperator operation, AbstractToken right)
            : base(new AbstractToken[] { left, operation, right }) { }
    }
}
