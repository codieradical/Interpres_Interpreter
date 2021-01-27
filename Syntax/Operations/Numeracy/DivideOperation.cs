using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations
{
    public class DivideOperatrion : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft / dynamicRight;
            }

            throw new SyntaxException($"Can't divide a {left.GetType()} by a {right.GetType()}");
        }
    }
}