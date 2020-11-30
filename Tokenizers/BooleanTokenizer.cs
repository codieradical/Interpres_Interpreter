using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class BooleanTokenizer : ITokenizer
    {
        public AbstractToken ParseToken(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("No token input!");

            string token = input.Split(null)[0];
            if (string.IsNullOrWhiteSpace(token))
                throw new TokenizationException("Empty token.", 0);

            if (token.Equals("true"))
            {
                return new ValueToken(true, token);
            }
            else if (token.Equals("false"))
            {
                return new ValueToken(false, token);
            }
            throw new TokenizationException("Not a boolean.", 0);
        }
    }
}
