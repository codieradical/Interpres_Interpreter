using Interpreter.Data;
using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.Commands
{
    class OpenCommand : Command
    {
        public override object Execute(object[] args)
        {
            Workspace file = new LocalFileService().OpenWorkspace();
            foreach (string line in file.script)
            {
                Console.WriteLine(line);
            }
            VariableStorage.singleton.LoadWorkspace(file);
            return "Opened file.";
        }

        public override string GetInputString()
        {
            return "open";
        }
    }
}
