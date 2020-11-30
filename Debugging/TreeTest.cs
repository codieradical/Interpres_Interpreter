using Interpres.Tokens;
using Interpres.Tokens.Expressions;
using Interpres_dev;
using System;
using System.Dynamic;
using System.Linq;
using System.Resources;

namespace Interpres.Debugging
{
    class TreeTest
    {
        public static void Debug(string[] args)
        {
            bool showTree = true;

            while (true)

            {
                Console.Write(">> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;

                if(line == "^showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Console displays parse trees" : "Not displaying parse tree");
                    continue;
                }
                else if(line == "^cls")
                {
                    Console.Clear();
                    continue;
                }


                var syntaxTree = SyntaxTree.Parse(line);

                if(showTree)
                {
                    // Parser tree debugging
                    PrintConsole(syntaxTree.Root);
                }
                

                // Error reporting
                if (!syntaxTree.Diag.Any())
                {
                    var eval = new Evaluator(syntaxTree.Root);
                    var result = eval.Evaluate();

                    Console.WriteLine(result);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var diag in syntaxTree.Diag)
                        Console.WriteLine(diag);

                    Console.ForegroundColor = ConsoleColor.White;
                }



                /*              Lexer debugging stuff 
                                var lexer = new Lexer(line);

                                while (true)
                                {
                                    var token = lexer.NextToken();
                                    if (token.Type == syntaxType.EOFToken)
                                        break;

                                    Console.Write($"{token.Type}: '{token.Text}'");

                                    if (token.Value != null)
                                        Console.Write($"{token.Value}");

                                    Console.WriteLine();
                                }
                */

            }

        }

        // Parser tree debugging stuff 
        static void PrintConsole(AbstractToken node, string indent = "", bool isLast = true)
        {
            // Parser tree display - use linux cmd tree notation
            // └──
            // |
            // ├──
            var marker = " ";

            if (isLast)
                marker = "└──";
            else
                marker = "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.GetType());

            if(node is AbstractToken t)
            {
                Console.Write(" ");
                Console.Write(t.GetInputString());
            }

            Console.WriteLine();

            if (isLast)
                indent += "    ";
            else
                indent += "|   ";


            if (node is Expression)
            {
                var lastChild = (node as Expression).expressionTokens.LastOrDefault();

                foreach (var child in (node as Expression).expressionTokens)
                {
                    PrintConsole(child, indent, child == lastChild);
                }
            }
        }
    }

}
