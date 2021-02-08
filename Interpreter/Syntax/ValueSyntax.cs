using Interpres.Tokens;

namespace Interpreter.Syntax
{
    public class ValueSyntax : AbstractSyntax
    {
        public object Value { get; private set; }

        protected ValueSyntax() { }
        
        public ValueSyntax(object value)
        {
            Value = value;
        }

        public ValueSyntax(ValueToken value)
        {
            Value = value.Value;
        }

        public override object GetValue()
        {
            return Value;
        }
    }
}