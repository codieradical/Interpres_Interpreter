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
    class OperatorTokenizer : ITokenizer
    {
        private Dictionary<string, AbstractOperator> operators = new Dictionary<string, AbstractOperator>(); 
        public OperatorTokenizer()
        {
            RegisterOperators();
        }

        private void RegisterOperators()
        {
            operators.Add("&", new Interpres.Tokens.Bitwise.AndOperator());
            operators.Add("~", new ComplementOperator());
            operators.Add("<<", new LeftShiftOperator());
            operators.Add("|", new Interpres.Tokens.Bitwise.OrOperator());
            operators.Add(">>", new RightShiftOperator());
            //operators.Add("^", new XOrOperator());

            operators.Add("&&", new Interpres.Tokens.Logical.AndOperator());
            operators.Add("!", new NotOperator());
            operators.Add("||", new Interpres.Tokens.Bitwise.OrOperator());

            operators.Add("+", new AddOperator());
            operators.Add("/", new DivideOperator());
            operators.Add("*", new MultiplyOperator());
            operators.Add("-", new SubtractOperator());

            operators.Add("%", new ModuloOperator());

            operators.Add("^", new PowerOperator());

            operators.Add("=", new AssignmentOperator());
        }

        public AbstractToken ParseToken(string input)
        {
            input = input.TrimStart();
            foreach (string key in operators.Keys)
            {
                if (input.StartsWith(key))
                    return operators[key];
            }
            throw new TokenizationException("Not a operator.", 0);
        }
    }
}
