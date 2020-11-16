using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    class WhitespaceToken : AbstractToken
    {
        string whitespace;
        public WhitespaceToken(int position, string whitespace)
        {
            this.whitespace = whitespace;
            Position = position;
        }

        public override string ToString()
        {
            return whitespace.ToString();
        }
    }
}
