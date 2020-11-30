using System;
using Interpreter.Data;

namespace Interpreter.Syntax.Operations
{
    public class AssignmentOperation : IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right)
        {
            if (!(left is IdentifierSyntax))
                throw new SyntaxException("Bad identifier " + left);
            
            VariableStorage.singleton.WriteVariable((left as IdentifierSyntax).Identifier, right.GetValue());

            return right.GetValue();
        }
    }
}