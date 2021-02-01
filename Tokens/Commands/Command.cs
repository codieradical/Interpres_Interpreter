using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Tokens.commands
{
    public abstract class Command : AbstractToken
    {
        public abstract object Execute(object[] args);
    }
}
