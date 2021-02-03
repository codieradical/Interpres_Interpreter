using Interpres.Tokens;
using Interpreter.Extensions;
using Interpreter.Tokens.commands;
using Interpreter.Tokens.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokenizers
{
    public class ConsoleCommandTokenizer : CommandTokenizer
    {
        protected override void RegisterCommands()
        {
            commands.Add(new ExitCommand());
            commands.Add(new OpenCommand());
            commands.Add(new SaveCommand());
        }
    }
}
