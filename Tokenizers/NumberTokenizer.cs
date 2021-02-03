using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class NumberTokenizer : ITokenizer
    {
        public AbstractToken ParseToken(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("No token input!");

            string token = input.Split(null)[0];
            if (string.IsNullOrWhiteSpace(token))
                throw new TokenizationException("Empty token.", 0);

            // Loop through and check where the number ends.
            for (int i = 0; i < token.Length; i++)
            {
                if (char.IsDigit(token[i]) || token[i] == '.')
                    continue;
                // hexidecimal prefix.
                if (i == 1 && token[i] == 'x' && token[0] == '0')
                    continue;
                if (token[i] == 'd' || token[i] == 'f' || token[i] == 'D' || token[i] == 'F')
                    i += 1;

                token = token.Substring(0, i);
                break;
            }

            if (token.StartsWith("0x"))
            {
                try
                {
                    int value = Convert.ToInt32(token.ToUpper(), 16);
                    return new ValueToken(value, token);
                }
                catch (Exception ex)
                {
                    throw new TokenizationException("Invalid hexidecimal value: " + ex.Message, 2);
                }
            }
            else if (token.ToLower().EndsWith("f"))
            {
                bool valid = float.TryParse(token.Substring(0, token.Length - 1), out float value);
                if (!valid)
                    throw new TokenizationException("Invalid float value.", 0);
                return new ValueToken(value, token);
            }
            else if (token.ToLower().EndsWith("d"))
            {
                bool valid = double.TryParse(token.Substring(0, token.Length - 1), out double value);
                if (!valid)
                    throw new TokenizationException("Invalid double value.", 0);
                return new ValueToken(value, token);
            }
            else if (token.Contains('.'))
            {
                bool valid = double.TryParse(token.Substring(0, token.Length), out double value);
                if (!valid)
                    throw new TokenizationException("Invalid double value.", 0);
                return new ValueToken(value, token);
            }
            else
            {
                bool valid = int.TryParse(token, out int value);
                if (!valid)
                    throw new TokenizationException("Invalid integer value.", 0);
                return new ValueToken(value, token);
            }
        }
    }
}
