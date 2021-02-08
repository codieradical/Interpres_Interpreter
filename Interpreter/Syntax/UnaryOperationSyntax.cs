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

namespace Interpreter.Syntax
{
    public class UnaryOperationSyntax<TOperand>: AbstractSyntax where TOperand : AbstractSyntax
    {
        private Dictionary<Type, IUnaryOperation> operations = new Dictionary<Type, IUnaryOperation>();
        private AbstractUnaryOperator abstractOperator;
        private TOperand operand;
        
        public UnaryOperationSyntax(AbstractUnaryOperator abstractOperator, TOperand operand)
        {
            this.abstractOperator = abstractOperator;
            this.operand = operand;
            RegisterOperations();
        }


        public override object GetValue()
        {
            return operations[abstractOperator.GetType()].Operate(operand);
        }

        public void RegisterOperations()
        {
            operations.Add(typeof(NotOperator), new NotOperation());
            operations.Add(typeof(ComplementOperator), new ComplementOperation());
        }
    }
}