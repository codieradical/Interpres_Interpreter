using Interpreter;
using Interpreter.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenizerService tokenizerService = new TokenizerService();
            Workspace workspace = new LocalFileWorkspace(null, new string[0]);

            while (true)
            {
                string input = Console.ReadLine();

                try
                {
                    var tokens = tokenizerService.GetTokens(input);
                    //foreach (object token in subtreeTokens)
                    //{
                    //    Console.WriteLine("token: " + token.GetType().ToString());
                    //    if (token is ValueToken)
                    //    {
                    //        Console.WriteLine("Value: " + ((ValueToken)token).Value);
                    //    }
                    //}


                    Console.WriteLine("ans: " + new AbstractSyntaxTree(tokens.Select(token => (object)token).ToList(), workspace).GetValue());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace.ToString());
                }
            }
        }
    }
}
