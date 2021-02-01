using Interpreter.Tokens.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Syntax
{
    public class CommandSyntax : AbstractSyntax
    {
        Command command;
        List<object> args;

        public CommandSyntax(Command command, List<object> subtreeTokens)
        {
            this.command = command;
            this.args = subtreeTokens;
        }

        public override object GetValue()
        {
            return command.Execute(args.ToArray());
        }
    }
}
