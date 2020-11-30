using Interpres.Tokens;
using Interpreter.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class IdentifierTokenizer : ITokenizer
    {
        public AbstractToken ParseToken(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("No token input!");

            string token = input.Split(null)[0];
            if (string.IsNullOrWhiteSpace(token))
                throw new TokenizationException("Empty token.", 0);

            // Identifiers must start with a letter.
            if (!char.IsLetter(token.ElementAt(0)))
                throw new TokenizationException("Invalid identifier name.", 0);

            string identifier = token[0].ToString();
            for(int i = 1; i < token.Length; i++)
            {
                // Identifiers can contain letters, numbers and underscores.
                if(char.IsLetterOrDigit(token[i]) || token[i] == '_')
                {
                    identifier = identifier + token[i];
                } else
                {
                    break;
                }
            }

            return new IdentifierToken(identifier);
        }
    }
}
