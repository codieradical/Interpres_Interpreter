using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Interpreter.Tokens.Commands
{
    class WriteFileCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            if (args.Length == 2 && args[0] is string)
            {
                FileStream fileStream = File.Open((string)args[0], FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(args[1].ToString());
                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();

                return "success.";
            } else
            {
                throw new ArgumentException("Invalid arguments");
            }
        }

        public override string GetInputString()
        {
            return "write_file";
        }
    }
}
