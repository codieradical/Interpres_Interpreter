using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    abstract class AbstractOperator : AbstractToken
    {
        public AbstractOperator(int position)
        {
            Position = position;
        }

        public abstract ValueToken Operate(params ValueToken[] values);
    }
}
