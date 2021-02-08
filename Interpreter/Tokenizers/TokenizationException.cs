using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class TokenizationException : Exception
    {
        public int Position { get; }

        public TokenizationException(string message, int position) : base($"Bad token at {position}: {message}") 
        {
            Position = position;
        }
    }
}
