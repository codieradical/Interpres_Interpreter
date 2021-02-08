using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Logical
{
    public class AndOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsBoolean() && right.GetValue().IsBoolean())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft && dynamicRight;
            }
            
            throw new SyntaxException($"Can't and a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
        }
    }
}