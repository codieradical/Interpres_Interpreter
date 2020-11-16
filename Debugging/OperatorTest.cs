using Interpres.Lexer;
using Interpres.Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpres.Debugging
{
    class OperatorTest
    {
        public static void Debug(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                    break;
                Console.WriteLine("Answer: " + LexerTest(input));
            }
        }

        public static object LexerTest(string lexeme)
        {
            LinkedList<AbstractToken> tokens = new LinkedList<AbstractToken>();

            for (int i = 0; i < lexeme.Length; i++)
            {
                if (lexeme[i] == ' ')
                {
                    continue;
                }

                AbstractOperator abstractOperator = OperatorService.GetOperator(i, lexeme.Substring(i));
                if (abstractOperator != null)
                {
                    tokens.AddLast(abstractOperator);
                    i += abstractOperator.ToString().Length;
                    continue;
                }

                if (lexeme[i] == '(')
                {
                    tokens.AddLast(new LeftParenthesisToken());
                    continue;
                }

                if (lexeme[i] == ')')
                {
                    tokens.AddLast(new RightParenthesisToken());
                    continue;
                }

                int tokenEnd = lexeme.Substring(i).IndexOf(" ");
                if (tokenEnd < 1)
                    tokenEnd = lexeme.Substring(i).Length;

                ValueToken valueToken = ValueToken.FromString(lexeme.Substring(i, tokenEnd));
                i += valueToken.ToString().Length - 1;

                tokens.AddLast(valueToken);
            }

            Stack<ValueToken> valueStack = new Stack<ValueToken>();

            for (int i = 0; i < tokens.Count; i++)
            {
                AbstractToken token = tokens.ElementAt(i);

                if (token is ValueToken)
                {
                    valueStack.Push(token as ValueToken);
                }

                if (token is AbstractOperator)
                {
                    for (; i + 1 < tokens.Count; i++)
                    {
                        AbstractToken nextToken = tokens.ElementAt(i + 1);

                        if (nextToken is ValueToken)
                        {
                            valueStack.Push(nextToken as ValueToken);
                        }
                        else
                        {
                            break;
                        }
                    }

                    ValueToken[] values = valueStack.Reverse().ToArray();

                    valueStack.Clear();
                    valueStack.Push((token as AbstractOperator).Operate(values));
                }
            }

            return valueStack.Pop().Value;
        }
    }
}
