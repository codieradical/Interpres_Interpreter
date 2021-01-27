using Interpreter.Extensions;
using System;

namespace Interpreter.Syntax.Operations
{
    public class PowerOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return Math.Pow(dynamicLeft, dynamicRight);
            }
            
            throw new SyntaxException($"Can't add a {left.GetType()} to a {right.GetType()}");
        }
    }
}