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
        public override object Execute(object[] args, Workspace workspace)
        {
            Workspace file = new LocalFileService().OpenWorkspace();
            foreach (string line in file.script)
            {
                Console.WriteLine(line);
            }
            return "Opened file.";
        }

        public override string GetInputString()
        {
            return "open";
        }
    }
}
