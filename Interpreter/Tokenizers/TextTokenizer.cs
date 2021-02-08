using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class TextTokenizer : ITokenizer
    {
        public AbstractToken ParseToken(string input)
        {
            // Parse a character.
            if (input.StartsWith("\'") && input.ElementAt(2) == '\'')
            {
                return new ValueToken(input.ElementAt(1), input.Substring(0, 3));
            }
            if (input.StartsWith("\""))
            {
                string value = input.Substring(1, input.Substring(1).IndexOf('\"'));
                return new ValueToken(value, $"\"{value}\"");
            }
            throw new TokenizationException("Not a string.", 0);
        }
    }
}
