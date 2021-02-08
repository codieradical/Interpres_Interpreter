using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Interpres.Tokens;

namespace Interpreter.Tokens.Commands
{
    class DeleteFileCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            if (args[0] is string)
                File.Delete((string)args[0]);
            return "success.";
        }

        public override string GetInputString()
        {
            return "delete_file";
        }
    }
}
