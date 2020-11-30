using Interpres;
using Interpres.Tokens;
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

                AbstractOperator @operator = OperatorService.GetOperator(i, lexeme.Substring(i));
                if (@operator != null)
                {
                    tokens.AddLast(@operator);
                    i += @operator.GetInputString().Length;
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

                ValueToken valueToken = null;
                i += valueToken.GetInputString().Length - 1;

                tokens.AddLast(valueToken);
            }

            Stack<ValueToken> valueStack = new Stack<ValueToken>();

            for (int i = 0; i < tokens.Count; i++)
            {
                AbstractToken abstractToken = tokens.ElementAt(i);

                if (abstractToken is ValueToken)
                {
                    valueStack.Push(abstractToken as ValueToken);
                }

                if (abstractToken is AbstractOperator)
                {
                    for (; i + 1 < tokens.Count; i++)
                    {
                        AbstractToken nextAbstractToken = tokens.ElementAt(i + 1);

                        if (nextAbstractToken is ValueToken)
                        {
                            valueStack.Push(nextAbstractToken as ValueToken);
                        }
                        else
                        {
                            break;
                        }
                    }

                    ValueToken[] values = valueStack.Reverse().ToArray();

                    valueStack.Clear();
                    valueStack.Push((abstractToken as AbstractOperator).Operate(values));
                }
            }

            return valueStack.Pop().Value;
        }
    }
}
