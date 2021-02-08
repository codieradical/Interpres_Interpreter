using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens
{
    public class IdentifierToken : AbstractToken
    {
        private readonly string input;
        public IdentifierToken(string input)
        {
            this.input = input;
        }

        public override string GetInputString()
        {
            return input;
        }
    }
}
