using Interpres.Tokens;
using Interpreter;
using Interpreter.Tokenizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.IO;

namespace Interpreter.Debugging
{
    public static class ASTTest
    {
        public static void Debug(string[] args)
        {
            TokenizerService tokenizerService = new TokenizerService();
                
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

                    Workspace workspace = new LocalFileWorkspace(null, new string[0]);

                    Console.WriteLine("ans: " + new AbstractSyntaxTree(tokens.Select(token => (object) token).ToList(), workspace).GetValue());
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace.ToString());
                }
            }
        }
    }
}
