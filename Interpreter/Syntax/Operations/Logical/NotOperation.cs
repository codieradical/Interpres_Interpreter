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
            } else if (operand.IsArray())
            {
                object[] result = (object[])((object[])operand.GetValue()).Clone();
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].IsBoolean())
                        result[i] = !(bool)result[i];
                    else if (result[i].IsArray())
                        result[i] = Operate(new ValueSyntax(result[i]));
                    else
                        throw new SyntaxException($"Can't not all elements in this matrix.");
                }

                return result;
            }
            
            throw new SyntaxException($"Can't not a {operand.GetValue().GetType()}.");
        }
    }
}