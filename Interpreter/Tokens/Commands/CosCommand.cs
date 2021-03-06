﻿using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Extensions;

namespace Interpreter.Tokens.Commands
{
    class CosCommand : Command
    {
        public override object Execute(object[] args, Workspace workspace)
        {
            Console.WriteLine(args[0].GetType());
            if (args[0].IsNumeric())
                return Math.Cos(double.Parse(args[0].ToString()));

            throw new ArgumentException("Invalid argument.");
        }

        public override string GetInputString()
        {
            return "cos";
        }
    }
}
