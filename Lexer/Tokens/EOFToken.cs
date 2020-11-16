using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    class EOFToken : AbstractToken
    {
        public EOFToken(int position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
