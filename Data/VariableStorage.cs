using Interpreter.IO;
using System.Collections.Generic;

namespace Interpreter.Data
{
    public class VariableStorage
    {
        public static VariableStorage singleton = new VariableStorage();
        
        private Dictionary<string, object> variables = new Dictionary<string, object>();

        public void WriteVariable(string name, object value)
        {
            variables[name] = value;
        }

        public object GetVariable(string name)
        {
            return variables[name];
        }

        public Dictionary<string, object>.KeyCollection GetVariableNames()
        {
            return variables.Keys;
        }

        public Workspace AsWorkspace()
        {
            return new Workspace(new string[0], variables);
        }

        public void LoadWorkspace(Workspace workspace)
        {
            variables = workspace.variables;
        }

    }
}