namespace Interpreter.Syntax.Operations
{
    public interface IUnaryOperation
    {
        public object Operate(AbstractSyntax operand);
    }
}