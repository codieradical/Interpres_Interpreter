using Interpreter.Extensions;
using System;

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
            else if (left.GetValue().IsString() || left.GetValue().IsCharacter())
            {
                return left.GetValue().ToString() + right.GetValue().ToString();
            }
            else if (left.GetValue().IsArray() && right.GetValue().IsNumeric())
            {
                try
                {
                    object[] result = (object[])((object[])left.GetValue()).Clone();
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i].IsNumeric())
                            result[i] = (dynamic)result[i] + (dynamic)right.GetValue();
                        else if (result[i].IsArray())
                            result[i] = Operate(new ValueSyntax(result[i]), new ValueSyntax(right.GetValue()));
                        else
                            throw new SyntaxException($"Can't add {right.GetValue().GetType()} to all elements in this matrix.");
                    }

                    return result;
                } catch
                {
                    throw new SyntaxException($"Can't add {right.GetValue().GetType()} to all elements in this matrix.");
                }
            }
            
            throw new SyntaxException($"Can't add a {left.GetValue().GetType()} to a {right.GetValue().GetType()}");
        }
    }
}