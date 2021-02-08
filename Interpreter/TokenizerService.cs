using Interpres.Tokens;
using Interpreter.Tokenizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class TokenizerService
    {
        private List<ITokenizer> tokenizers = new List<ITokenizer>();

        public TokenizerService(CommandTokenizer commandTokenizer)
        {
            RegisterTokenizers(commandTokenizer);
        }

        public TokenizerService()
        {
            RegisterTokenizers(new CommandTokenizer());
        }

        public List<AbstractToken> GetTokens(string input)
        {
            List<AbstractToken> tokens = new List<AbstractToken>();

            string remaining = input.TrimStart();

            while (remaining.Length > 0)
            {
                if (string.IsNullOrWhiteSpace(remaining))
                    return tokens;

                int remainingLength = remaining.Length;

                foreach (ITokenizer tokenizer in tokenizers)
                {
                    try
                    {
                        AbstractToken abstractToken = tokenizer.ParseToken(remaining);
                        tokens.Add(abstractToken);
                        remaining = remaining.Substring(abstractToken.GetInputString().Length);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if(!(ex is TokenizationException))
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace.ToString());
                        }

                        continue;
                    }
                }
                // If no token has been found throw.
                if (remaining.Length == remainingLength)
                {
                    throw new TokenizationException("Could not parse token " + remaining, input.Length - remaining.Length);
                }
                remaining = remaining.TrimStart();
            }

            return tokens;
        }

        private void RegisterTokenizers(CommandTokenizer commandTokenizer)
        {
            // Order can be important here.
            tokenizers.Add(new NumberTokenizer());
            tokenizers.Add(commandTokenizer);
            tokenizers.Add(new OperatorTokenizer());
            tokenizers.Add(new TextTokenizer());
            tokenizers.Add(new BooleanTokenizer());
            tokenizers.Add(new SyntaxTokenizer());
            // This one is least strict so must be last.
            tokenizers.Add(new IdentifierTokenizer());
        }
    }
}
