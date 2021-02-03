using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Bitwise
{
    public class XOrOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft ^ dynamicRight;
            }
            
            throw new SyntaxException($"Can't xor a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
        }
    }
}