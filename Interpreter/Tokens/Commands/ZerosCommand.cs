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
    class ZerosCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            if (args[0].IsNumeric() && args.Length == 1)
            {
                object[] res = new object[(int)args[0]];
                for (int i = 0; i < (int)args[0]; i++)
                {
                    res[i] = 0;
                }
                return res;
            }
            if (args[0].IsNumeric() && args[1].IsNumeric() && args.Length == 2)
            {
                object[] res = new object[(int)args[0]];
                for (int i = 0; i < (int)args[0]; i++)
                {
                    res[i] = Execute(new object[] { args[1] }, workspace);
                }
                return res;
            }

            throw new ArgumentException("Invalid arguments.");
        }

        public override string GetInputString()
        {
            return "zeros";
        }
    }
}
