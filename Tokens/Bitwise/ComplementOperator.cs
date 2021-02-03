using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens.Bitwise
{
    class ComplementOperator : AbstractUnaryOperator
    {

        public override string GetInputString()
        {
            return "~";
        }
    }
}
