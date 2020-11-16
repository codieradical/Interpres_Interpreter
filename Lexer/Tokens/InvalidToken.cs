using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    class InvalidToken : AbstractToken
    {
        readonly string token;

        public InvalidToken(int position, string token)
        {
            Position = position;
            this.token = token;
        }
        public override string ToString()
        {
            return token;
        }
    }
}
