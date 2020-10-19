using Interpreter.lexer.tokens.bitwise;
using Interpreter.lexer.tokens.logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.lexer.tokens
{
    class OperatorService
    {
        private static OperatorService _singleton;
        public static OperatorService singleton
        {
            get
            {
                if(_singleton == null)
                    _singleton = new OperatorService();

                return _singleton;
            }
            private set
            {
                _singleton = value;
            }
        }

        LinkedList<AbstractOperator> operators;

        public void RegisterOperator(AbstractOperator newOperator)
        {
            foreach (AbstractOperator registeredOperator in operators)
            {
                if (registeredOperator.ToString().Equals(newOperator.ToString()))
                {
                    throw new Exception($"Operator {newOperator.GetType()} clashes with registered operator {registeredOperator.GetType()}");
                }
            }

            operators.AddLast(newOperator);
        }

        OperatorService()
        {
            operators = new LinkedList<AbstractOperator>();

            // Bitwise
            RegisterOperator(new bitwise.AndOperator());
            RegisterOperator(new ComplementOperator());
            RegisterOperator(new LeftShiftOperator());
            RegisterOperator(new bitwise.OrOperator());
            RegisterOperator(new RightShiftOperator());
            //RegisterOperator(new XOrOperator());

            // Logical
            RegisterOperator(new logical.AndOperator());
            RegisterOperator(new NotOperator());
            RegisterOperator(new logical.OrOperator());

            // Numeracy
            RegisterOperator(new AddOperator());
            RegisterOperator(new DivideOperator());
            RegisterOperator(new ModuloOperator());
            RegisterOperator(new MultiplyOperator());
            RegisterOperator(new PowerOperator());
            RegisterOperator(new SubtractOperator());
        }

        public static AbstractOperator GetOperator(string lexeme)
        {
            foreach (AbstractOperator registeredOperator in singleton.operators)
            {
                if (lexeme.StartsWith(registeredOperator.ToString() + " "))
                {
                    return registeredOperator;
                }
            }

            return null;
        }
    }
}
