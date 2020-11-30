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
        public override void Execute()
        {
            Environment.Exit(0);
        }

        public override string GetInputString()
        {
            return "exit";
        }
    }
}
