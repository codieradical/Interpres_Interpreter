using System;
using System.Collections.Generic;
using Interpres.Tokens;
using Interpres.Tokens.Numeracy;
using Interpreter.Syntax.Operations;

namespace Interpreter.Syntax
{
    public class BinaryOperationSyntax<TLeft, TRight>: AbstractSyntax where TLeft : AbstractSyntax where TRight : AbstractSyntax
    {
        private Dictionary<Type, IBinaryOperation> operations = new Dictionary<Type, IBinaryOperation>();
        private AbstractOperator abstractOperator;
        private TLeft left;
        private TRight right;
        
        public BinaryOperationSyntax(AbstractOperator abstractOperator, TLeft left, TRight right)
        {
            this.abstractOperator = abstractOperator;
            this.left = left;
            this.right = right;
            RegisterOperations();
        }


        public override object GetValue()
        {
            return operations[abstractOperator.GetType()].Operate(left, right);
        }

        public void RegisterOperations()
        {
            operations.Add(typeof(AddOperator), new AddOperation());
            operations.Add(typeof(SubtractOperator), new SubtractOperation());
            operations.Add(typeof(AssignmentOperator), new AssignmentOperation());
            operations.Add(typeof(MultiplyOperator), new MultiplyOperation());
        }
    }
}