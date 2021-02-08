using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.IO
{
    [Serializable]
    public abstract class Workspace
    {
        public Workspace(string[] script)
        {
            this.script = script;
        }

        public Workspace(string[] script, Dictionary<string, object> variables)
        {
            this.script = script;
            this.variables = variables;
        }

        public Dictionary<string, object> variables = new Dictionary<string, object>();
        public string[] script;
        [NonSerialized]
        public LinkedList<string> commandLog = new LinkedList<string>();
        [NonSerialized]
        public string command = "";
        [NonSerialized]
        public bool loading = false;

        public abstract void SaveWorkspace();
    }
}
