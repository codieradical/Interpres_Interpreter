using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations
{
    public class SubtractOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft - dynamicRight;
            }

            throw new SyntaxException($"Can't subtract a {right.GetType()} from a {left.GetType()}");
        }
    }
}