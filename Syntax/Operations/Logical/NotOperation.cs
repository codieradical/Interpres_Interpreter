using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Logical
{
    public class NotOperation : IUnaryOperation
    {
        public object Operate(AbstractSyntax operand)
        {
            if (operand.GetValue().IsBoolean())
            {
                dynamic dynamicOperand = operand.GetValue();
                return !dynamicOperand;
            }
            
            throw new SyntaxException($"Can't not a {operand.GetValue().GetType()}.");
        }
    }
}