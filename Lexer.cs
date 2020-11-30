using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Lexer
    {
        private string input;
        private readonly List<AbstractToken> tokens;

        public ReadOnlyCollection<AbstractToken> Tokens
        {
            get {
                return new ReadOnlyCollection<AbstractToken>(tokens);
            }
        }

        public Lexer(string input)
        {
            this.input = input;


        }
    }
}
