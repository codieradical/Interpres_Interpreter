using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    class EofAbstractToken : AbstractToken
    {
        public EofAbstractToken()
        {
        }

        public override string GetInputString()
        {
            return "";
        }
    }
}
