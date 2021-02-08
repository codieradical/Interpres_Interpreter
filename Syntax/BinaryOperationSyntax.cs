using System;
using System.Collections.Generic;
using Interpres.Tokens;
using Interpres.Tokens.Bitwise;
using Interpres.Tokens.Logical;
using Interpres.Tokens.Numeracy;
using Interpreter.Syntax.Operations;
using Interpreter.Syntax.Operations.Bitwise;
using Interpreter.Syntax.Operations.Logical;
using Interpreter.Syntax.Operations.Numeracy;
using Interpreter.Tokens.Matrix;

namespace Interpreter.Syntax
{
    public class BinaryOperationSyntax<TLeft, TRight>: AbstractSyntax where TLeft : AbstractSyntax where TRight : AbstractSyntax
    {
        private Dictionary<Type, IBinaryOperation> operations = new Dictionary<Type, IBinaryOperation>();
        private AbstractBinaryOperator abstractOperator;
        private TLeft left;
        private TRight right;
        
        public BinaryOperationSyntax(AbstractBinaryOperator abstractOperator, TLeft left, TRight right)
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
            operations.Add(typeof(AssignmentOperator), new AssignmentOperation());

            operations.Add(typeof(AddOperator), new AddOperation());
            operations.Add(typeof(SubtractOperator), new SubtractOperation());
            operations.Add(typeof(MultiplyOperator), new MultiplyOperation());
            operations.Add(typeof(DivideOperator), new DivideOperatrion());
            operations.Add(typeof(PowerOperator), new PowerOperation());
            operations.Add(typeof(ModuloOperator), new ModuloOperation());

            operations.Add(typeof(Interpres.Tokens.Logical.AndOperator), new Operations.Logical.AndOperation());
            operations.Add(typeof(Interpres.Tokens.Logical.OrOperator), new Operations.Logical.OrOperation());

            operations.Add(typeof(Interpres.Tokens.Bitwise.AndOperator), new Operations.Bitwise.AndOperation());
            operations.Add(typeof(Interpres.Tokens.Bitwise.OrOperator), new Operations.Bitwise.OrOperation());
            operations.Add(typeof(LeftShiftOperator), new LeftShiftOperation());
            operations.Add(typeof(RightShiftOperator), new RightShiftOperation());
            operations.Add(typeof(XOrOperator), new XOrOperation());

            operations.Add(typeof(ElementWiseAddOperator), new ElementWiseAddOperation());
            operations.Add(typeof(ElementWiseMultiplyOperator), new ElementWiseMultiplyOperation());
            operations.Add(typeof(ElementWiseSubtractOperator), new ElementWiseSubtractOperation());
            operations.Add(typeof(ElementWiseDivideOperator), new ElementWiseDivideOperation());
            operations.Add(typeof(ElementWiseAndOperator), new ElementWiseAndOperation());

        }
    }
}