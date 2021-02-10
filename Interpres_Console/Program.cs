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
        private static string ArrayToString(object[] array)
        {
            string[] valueStrings = new string[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                valueStrings[i] = array[i].ToString();
                if (array[i].GetType().IsArray)
                    valueStrings[i] = ArrayToString((object[])array[i]);
            }
            return "[" + string.Join(", ", valueStrings) + "]";
        }

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
                    object answer = new AbstractSyntaxTree(tokens.Select(token => (object)token).ToList(), workspace).GetValue();

                    string answerString = answer.ToString();

                    if (answer.GetType().IsArray)
                        answerString = ArrayToString((object[])answer);

                    Console.WriteLine("ans: " + answerString);
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
