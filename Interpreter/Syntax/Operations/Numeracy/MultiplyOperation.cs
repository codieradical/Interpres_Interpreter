using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Numeracy
{
    public class MultiplyOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsNumeric() && right.GetValue().IsNumeric())
            {
                dynamic dynamicLeft = left.GetValue();
                dynamic dynamicRight = right.GetValue();
                return dynamicLeft * dynamicRight;
            }
            else if (left.GetValue().IsArray() && right.GetValue().IsNumeric())
            {
                try
                {
                    object[] result = (object[])((object[])left.GetValue()).Clone();
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i].IsNumeric())
                            result[i] = (dynamic)result[i] * (dynamic)right.GetValue();
                        else if (result[i].IsArray())
                            result[i] = Operate(new ValueSyntax(result[i]), new ValueSyntax(right.GetValue()));
                        else
                            throw new SyntaxException($"Can't multiply all elements in this matrix by an {right.GetValue().GetType()}.");
                    }

                    return result;
                }
                catch
                {
                    throw new SyntaxException($"Can't multiply all elements in this matrix by {right.GetValue().GetType()}. ");
                }
            }

            throw new SyntaxException($"Can't multiply a {left.GetType()} by an {right.GetType()}");
        }
    }
}