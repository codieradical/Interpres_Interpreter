using Interpres.Tokens;
using Interpreter.Data;
using Interpreter.Tokens;

namespace Interpreter.Syntax
{
    public class IdentifierSyntax : ValueSyntax
    {
        public string Identifier { private set; get; }
        public IdentifierSyntax(IdentifierToken value)
        {
            Identifier = value.GetInputString();
        }
        
        public override object GetValue()
        {
            return VariableStorage.singleton.GetVariable(Identifier);
        }
    }
}