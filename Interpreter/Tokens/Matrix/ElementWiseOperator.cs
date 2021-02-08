using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.Matrix
{
    class ElementWiseOperator<T> : AbstractBinaryOperator where T : AbstractOperator, new()
    {
        public override string GetInputString()
        {
            return "." + new T().GetInputString();
        }
    }
}
