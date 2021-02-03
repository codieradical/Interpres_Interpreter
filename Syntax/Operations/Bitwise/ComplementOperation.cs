using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Bitwise
{
    public class ComplementOperation : IUnaryOperation
    {
        public object Operate(AbstractSyntax operand)
        {
            if (operand.GetValue().IsNumeric())
            {
                dynamic dynamicOperand = operand.GetValue();
                return ~dynamicOperand;
            }
            
            throw new SyntaxException($"Can't complement a {operand.GetValue().GetType()}.");
        }
    }
}