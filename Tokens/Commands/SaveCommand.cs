﻿using Interpreter.Data;
using Interpreter.IO;
using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.Commands
{
    class SaveCommand : Command
    {
        public override object Execute(object[] args)
        {
            new LocalFileService().SaveWorkspace(VariableStorage.singleton.AsWorkspace());

            return "Saved file.";
        }

        public override string GetInputString()
        {
            return "save";
        }
    }
}
