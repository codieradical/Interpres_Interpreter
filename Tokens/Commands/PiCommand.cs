using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.Commands
{
    class PiCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            return Math.PI;
        }

        public override string GetInputString()
        {
            return "pi";
        }
    }
}
