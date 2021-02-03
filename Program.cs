using Interpreter.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres
{
    class Program
    {  
        [STAThread]
        static void Main (string[] args)
        {
            ASTTest.Debug(args);
        }
    }
}
