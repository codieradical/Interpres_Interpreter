using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Numeracy
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
            if ((left.GetValue().IsString() || left.GetValue().IsCharacter()) && (right.GetValue().IsString() || right.GetValue().IsCharacter()))
            {
                return left.GetValue().ToString() + right.GetValue().ToString();
            }
            
            throw new SyntaxException($"Can't add a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
        }
    }
}