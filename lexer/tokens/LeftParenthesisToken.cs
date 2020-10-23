using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.lexer
{
    class LeftParenthesisToken : AbstractToken
    {
        public override string ToString()
        {
            return "(";
        }
    }
}
