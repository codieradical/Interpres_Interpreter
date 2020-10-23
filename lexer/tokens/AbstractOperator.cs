using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.lexer
{
    abstract class AbstractOperator : AbstractToken
    {
        public abstract ValueToken Operate(params ValueToken[] values);
    }
}
