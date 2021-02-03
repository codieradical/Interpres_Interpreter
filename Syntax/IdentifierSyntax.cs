using Interpres.Tokens;
using Interpreter.IO;
using Interpreter.Tokens;

namespace Interpreter.Syntax
{
    public class IdentifierSyntax : ValueSyntax
    {
        public string Identifier { private set; get; }
        public readonly Workspace workspace;
        public IdentifierSyntax(IdentifierToken value, Workspace workspace)
        {
            this.workspace = workspace;
            Identifier = value.GetInputString();
        }
        
        public override object GetValue()
        {
            return workspace.variables[Identifier];
        }

        public void SetValue(object value)
        {
            workspace.variables[Identifier] = value;
        }
    }
}