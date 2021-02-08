using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens.Numeracy
{
    class SubtractOperator : AbstractBinaryOperator
    {
        public override string GetInputString()
        {
            return "-";
        }
    }
}
