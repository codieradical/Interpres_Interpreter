using System;

namespace Interpreter.Syntax.Operations
{
    public class AssignmentOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (!(left is IdentifierSyntax))
                throw new SyntaxException("Bad identifier " + left);

            (left as IdentifierSyntax).SetValue(right.GetValue());

            return right.GetValue();
        }
    }
}