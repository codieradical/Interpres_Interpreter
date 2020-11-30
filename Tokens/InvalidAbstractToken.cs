using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    class InvalidAbstractToken : AbstractToken
    {
        readonly string token;

        public InvalidAbstractToken(string token)
        {
            this.token = token;
        }
        public override string GetInputString()
        {
            return token;
        }
    }
}
