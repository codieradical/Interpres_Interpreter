using Interpreter.Extensions;

namespace Interpreter.Syntax.Operations.Logical
{
    public class ElementWiseAndOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (left.GetValue().IsArray() && right.GetValue().IsArray())
            {
                object[] leftArray = (object[])left.GetValue();
                object[] rightArray = (object[])right.GetValue();

                if (leftArray.Length != rightArray.Length)
                    throw new SyntaxException($"Can't element-wise and matrices of differing sizes.");

                try
                {
                    object[] result = (object[])leftArray.Clone();
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i].IsBoolean() && rightArray[i].IsBoolean())
                            result[i] = (bool)result[i] && (bool)rightArray[i];
                        else if (result[i].IsArray() && rightArray[i].IsArray())
                            result[i] = Operate(new ValueSyntax(result[i]), new ValueSyntax(rightArray[i]));
                        else
                            throw new SyntaxException($"Can't element-wise and a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
                    }

                    return result;
                }
                catch
                {
                    throw new SyntaxException($"Can't element-wise and a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
                }
            }

            throw new SyntaxException($"Can't element-wise and a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
        }
    }
}