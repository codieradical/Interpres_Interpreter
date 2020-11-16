using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Lexer.Tokens
{
    abstract class AbstractToken
    {
        public abstract override string ToString();

        public int Position { get; set; }
    }
}
