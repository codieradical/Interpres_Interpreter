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
    class MatrixConcatenateCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            if (args[0].IsArray() && args[1].IsArray())
                return ((object[])args[0]).Concat((object[])args[1]).ToArray();

            throw new ArgumentException("Invalid arguments.");
        }

        public override string GetInputString()
        {
            return "matcat";
        }
    }
}
