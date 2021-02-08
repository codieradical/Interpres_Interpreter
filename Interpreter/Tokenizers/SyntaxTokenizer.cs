using Interpres.Tokens;
using Interpres.Tokens.Bitwise;
using Interpres.Tokens.Logical;
using Interpres.Tokens.Numeracy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class SyntaxTokenizer : ITokenizer
    {
        private Dictionary<string, AbstractToken> syntax = new Dictionary<string, AbstractToken>(); 
        public SyntaxTokenizer()
        {
            RegisterSyntaxTokens();
        }

        private void RegisterSyntaxTokens()
        {
            syntax.Add("(", new LeftParenthesisToken());
            syntax.Add(")", new RightParenthesisToken());
            syntax.Add("[", new LeftSquareParenthesisToken());
            syntax.Add("]", new RightSquareParenthesisToken());
            syntax.Add(",", new CommaToken());
        }

        public AbstractToken ParseToken(string input)
        {
            input = input.TrimStart();
            foreach (string key in syntax.Keys)
            {
                if (input.StartsWith(key))
                    return syntax[key];
            }
            throw new TokenizationException("Not a syntax token.", 0);
        }
    }
}
