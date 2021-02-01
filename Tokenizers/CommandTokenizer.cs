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
    class CommandTokenizer : ITokenizer
    {
        protected List<Command> commands = new List<Command>();

        public CommandTokenizer()
        {
            RegisterCommands();
        }

        public void RegisterCommands()
        {
            commands.Add(new ExitCommand());
            commands.Add(new OpenCommand());
            commands.Add(new SaveCommand());
        }

        public AbstractToken ParseToken(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("No token input!");

            string[] split = input.Split(null);


            string commandName = split[0];
            string[] arguments = split.SubArray(0);

            if (string.IsNullOrWhiteSpace(commandName))
                throw new TokenizationException("Empty token.", 0);

            foreach (Command command in commands)
            {
                if (commandName.ToLower().StartsWith(command.GetInputString()))
                {
                    return command;
                }
            }

            throw new TokenizationException("Not a command.", 0);
        }
    }
}
