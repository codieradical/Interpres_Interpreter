using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Extensions;

namespace Interpreter.Tokens.Commands
{
    class TanCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            if (args[0].IsNumeric())
                return Math.Tan(double.Parse(args[0].ToString()));

            throw new ArgumentException("Invalid argument.");
        }

        public override string GetInputString()
        {
            return "tan";
        }
    }
}
