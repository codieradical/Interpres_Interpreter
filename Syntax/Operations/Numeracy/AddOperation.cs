using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations
{
    public class AddOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft + dynamicRight;
            }
            if ((left.IsString() || left.IsCharacter()) && (right.IsString() || right.IsCharacter()))
            {
                return left.ToString() + right.ToString();
            }
            
            throw new SyntaxException($"Can't add a {left.GetType()} to a {right.GetType()}");
        }
    }
}