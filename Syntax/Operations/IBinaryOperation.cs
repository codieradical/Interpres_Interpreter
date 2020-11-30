namespace Interpreter.Syntax.Operations
{
    public interface IBinaryOperation
    {
        public object Operate(AbstractSyntax left, AbstractSyntax right);
    }
}