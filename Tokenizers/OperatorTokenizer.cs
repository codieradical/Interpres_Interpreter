using Interpres.Tokens;
using Interpres.Tokens.Bitwise;
using Interpres.Tokens.Logical;
using Interpres.Tokens.Numeracy;
using Interpreter.Tokens.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    class OperatorTokenizer : ITokenizer
    {
        private List<AbstractOperator> operators = new List<AbstractOperator>(); 
        public OperatorTokenizer()
        {
            RegisterOperators();
        }

        private void RegisterOperators()
        {
            operators.Add(new Interpres.Tokens.Logical.AndOperator());
            operators.Add(new NotOperator());
            operators.Add(new Interpres.Tokens.Bitwise.OrOperator());

            operators.Add(new Interpres.Tokens.Bitwise.AndOperator());
            operators.Add(new ComplementOperator());
            operators.Add(new LeftShiftOperator());
            operators.Add(new Interpres.Tokens.Bitwise.OrOperator());
            operators.Add(new RightShiftOperator());
            operators.Add(new XOrOperator());

            operators.Add(new AddOperator());
            operators.Add(new DivideOperator());
            operators.Add(new MultiplyOperator());
            operators.Add(new SubtractOperator());

            operators.Add(new ModuloOperator());

            operators.Add(new PowerOperator());

            operators.Add(new AssignmentOperator());

            operators.Add(new ElementWiseAddOperator());
            operators.Add(new ElementWiseSubtractOperator());
            operators.Add(new ElementWiseMultiplyOperator());
            operators.Add(new ElementWiseDivideOperator());
            operators.Add(new ElementWiseAndOperator());

        }

        public AbstractToken ParseToken(string input)
        {
            input = input.TrimStart();
            foreach (AbstractOperator _operator in operators)
            {
                if (input.StartsWith(_operator.GetInputString()))
                    return _operator;
            }
            throw new TokenizationException("Not a operator.", 0);
        }
    }
}
