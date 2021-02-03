using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.Commands
{
    class ExitCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            Environment.Exit(0);
            return "Exiting...";
        }

        public override string GetInputString()
        {
            return "exit";
        }
    }
}
