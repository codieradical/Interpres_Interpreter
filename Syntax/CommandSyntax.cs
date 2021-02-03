using Interpres.Tokens;
using Interpreter.IO;
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
        Workspace workspace;

        public CommandSyntax(Command command, List<object> subtreeTokens, Workspace workspace)
        {
            this.command = command;
            this.args = new List<object>();
            this.workspace = workspace;

            List<object> argSubtree = new List<object>();
            foreach (object token in subtreeTokens)
            {
                Console.WriteLine(token.GetType());
                if (token is CommaToken)
                {
                    args.Add(new AbstractSyntaxTree(argSubtree, workspace).GetValue());
                    argSubtree.Clear();
                }
                else
                    argSubtree.Add(token);
            }

            if (argSubtree.Count > 0)
            {
                args.Add(new AbstractSyntaxTree(argSubtree, workspace).GetValue());
            }
        }

        public override object GetValue()
        {
            return command.Execute(args.ToArray(), workspace);
        }
    }
}
