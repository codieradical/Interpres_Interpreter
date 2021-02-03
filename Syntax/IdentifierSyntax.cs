using Interpres.Tokens;
using Interpreter.IO;
using Interpreter.Tokens;
using System.Collections.Generic;

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
            try
            {
                return workspace.variables[Identifier];
            } catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Variable \"" + Identifier + "\" not found.");
            }
        }

        public void SetValue(object value)
        {
            workspace.variables[Identifier] = value;
        }
    }
}