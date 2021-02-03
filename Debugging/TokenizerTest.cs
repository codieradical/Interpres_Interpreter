using Interpres.Tokens;
using Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Debugging
{
    public static class TokenizerTest
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
                    foreach (AbstractToken token in tokens)
                    {
                        Console.WriteLine("token: " + token.GetInputString() + " " + token.GetType().ToString());
                        if (token is ValueToken)
                        {
                            Console.WriteLine("Value: " + ((ValueToken)token).Value);
                        }
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
