using Interpres.Tokens.Bitwise;
using Interpres.Tokens.Logical;
using Interpres.Tokens.Numeracy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Tokens
{
    class OperatorService
    {
        //private static OperatorService _singleton;
        //public static OperatorService singleton
        //{
        //    get
        //    {
        //        if(_singleton == null)
        //            _singleton = new OperatorService();

        //        return _singleton;
        //    }
        //    private set
        //    {
        //        _singleton = value;
        //    }
        //}

        //LinkedList<AbstractOperator> operators;

        //public void RegisterOperator(AbstractOperator newOperator)
        //{
        //    foreach (AbstractOperator registeredOperator in operators)
        //    {
        //        if (registeredOperator.ToString().Equals(newOperator.ToString()))
        //        {
        //            throw new Exception($"Operator {newOperator.GetType()} clashes with registered operator {registeredOperator.GetType()}");
        //        }
        //    }

        //    operators.AddLast(newOperator);
        //}

        //OperatorService()
        //{
        //    operators = new LinkedList<AbstractOperator>();

        //    // Bitwise
        //    RegisterOperator(new Bitwise.AndOperator());
        //    RegisterOperator(new ComplementOperator());
        //    RegisterOperator(new LeftShiftOperator());
        //    RegisterOperator(new Bitwise.OrOperator());
        //    RegisterOperator(new RightShiftOperator());
        //    //RegisterOperator(new XOrOperator());

        //    // Logical
        //    RegisterOperator(new Logical.AndOperator());
        //    RegisterOperator(new NotOperator());
        //    RegisterOperator(new Logical.OrOperator());

        //    // Numeracy
        //    RegisterOperator(new AddOperator());
        //    RegisterOperator(new DivideOperator());
        //    RegisterOperator(new ModuloOperator());
        //    RegisterOperator(new MultiplyOperator());
        //    RegisterOperator(new PowerOperator());
        //    RegisterOperator(new SubtractOperator());
        //}

        private static readonly Dictionary<string, Type> OPERATORS = new Dictionary<string, Type>
        {
            { "&", typeof(Bitwise.AndOperator) },
            { "~", typeof(ComplementOperator) },
            { "<<", typeof(LeftShiftOperator) },
            { "|", typeof(Bitwise.OrOperator) },
            { ">>", typeof(RightShiftOperator) },
            //{ "^", typeof(XOrOperator) },

            { "&&", typeof(Logical.AndOperator) },
            { "!", typeof(NotOperator) },
            { "||", typeof(Logical.OrOperator) },

            { "+", typeof(AddOperator) },
            { "/", typeof(DivideOperator) },
            { "*", typeof(MultiplyOperator) },
            { "-", typeof(SubtractOperator) },

            { "%", typeof(ModuloOperator) },

            { "^", typeof(PowerOperator) },
            { "=", typeof(AssignmentOperator) }
        };

        public static AbstractOperator GetOperator(int position, string lexeme)
        {
            try
            {
                AbstractOperator @operator = OPERATORS[lexeme].GetConstructor(new Type[] { typeof(int) }).Invoke(new object[] { position }) as AbstractOperator;

                return @operator;
            } catch(KeyNotFoundException ex)
            {
                return null;
            }
        }
    }
}
