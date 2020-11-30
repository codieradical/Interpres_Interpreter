using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    class WhitespaceAbstractToken : AbstractToken
    {
        string whitespace;
        public WhitespaceAbstractToken(string whitespace)
        {
            this.whitespace = whitespace;
        }

        public override string GetInputString()
        {
            return whitespace.ToString();
        }
    }
}
