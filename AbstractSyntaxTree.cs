using Interpres.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpres.Tokens.Numeracy;
using Interpreter.Extensions;
using Interpreter.Syntax;
using Interpreter.Tokens;
using Interpreter.Tokens.commands;
using Interpreter.IO;

namespace Interpreter
{
    public class AbstractSyntaxTree : AbstractSyntax
    {
        private AbstractSyntax root;
        private Workspace workspace;

        public AbstractSyntaxTree(List<object> tokens, Workspace workspace)
        {
            this.workspace = workspace;
            BuildTree(tokens);
        }

        public AbstractSyntaxTree(List<object> tokens)
        {
            BuildTree(tokens);
        }

        private void BuildTree(List<object> tokens)
        {
            // Contains syntax and tokens.
            //List<AbstractSyntax> workingList = new List<AbstractSyntax>();

            // Stores a mix of tokens and syntax.
            List<object> syntaxList = new List<object>();

            // 1. Parse Subtrees
            // Brackets (B)IDMAS
            int braceCount = 0;
            Command command = null;
            List<object> subtreeTokens = new List<object>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is LeftParenthesisToken)
                {
                    if (braceCount == 0 && i > 0 && tokens[i - 1] is Command)
                        command = tokens[i - 1] as Command;
                    braceCount += 1;
                }
                else if (tokens[i] is RightParenthesisToken)
                {
                    braceCount -= 1;
                    if (braceCount == 0)
                    {
                        if (command != null)
                        {
                            //syntaxList.Remove(syntaxList.Last());
                            syntaxList.Add(new CommandSyntax(command, subtreeTokens, workspace));
                            command = null;
                            //if (i - 2 == 0 || root == null)
                            //    root = syntaxList.First() as CommandSyntax;
                        }
                        else if (i < tokens.Count - 1 || syntaxList.Count > 0)
                            syntaxList.Add(new AbstractSyntaxTree(subtreeTokens));
                        else
                            syntaxList = subtreeTokens;
                    }

                    if (braceCount < 0)
                        throw new SyntaxException("Unexpected closing parenthesis.");
                }
                else if (braceCount > 0)
                    subtreeTokens.Add(tokens[i]);
                else
                    syntaxList.Add(tokens[i]);
            }
            
            // 2. Parse Values
            for (int i = 0; i < syntaxList.Count; i++)
            {
                if (syntaxList[i] is ValueToken)
                    syntaxList[i] = new ValueSyntax((ValueToken)syntaxList[i]);
                if (syntaxList[i] is IdentifierToken)
                {
                    if (workspace != null)
                        syntaxList[i] = new IdentifierSyntax((IdentifierToken)syntaxList[i], workspace);
                    else
                        throw new SyntaxException("Can't use variables with no workspace.");
                }
            }

            if (syntaxList.Count == 1 && syntaxList.First() is ValueSyntax)
            {
                root = syntaxList.First() as ValueSyntax;
                return;
            }

            tokens = syntaxList;
            syntaxList = new List<object>();
            braceCount = 0;
            List<AbstractSyntax> matrixTokens = new List<AbstractSyntax>();
            subtreeTokens = new List<object>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is LeftSquareParenthesisToken)
                    braceCount += 1;
                else if (tokens[i] is RightSquareParenthesisToken)
                {
                    braceCount -= 1;
                    if (braceCount == 0)
                    {
                        syntaxList.Add(new MatrixSyntax(matrixTokens));
                        if (syntaxList.Count == 1)
                            root = syntaxList.First() as MatrixSyntax;
                    }
                    else if (braceCount == 1)
                    {
                        List<object> subtreeMatrixTokens = new List<object>();
                        subtreeMatrixTokens.Add(new LeftSquareParenthesisToken());
                        subtreeMatrixTokens.AddRange(subtreeTokens);
                        subtreeMatrixTokens.Add(new RightSquareParenthesisToken());

                        matrixTokens.Add(new AbstractSyntaxTree(subtreeMatrixTokens));
                        subtreeTokens.Clear();
                    }

                    if (braceCount < 0)
                        throw new SyntaxException("Unexpected closing parenthesis.");
                }
                else if (braceCount == 1)
                {
                    if (tokens[i] is AbstractSyntax)
                        matrixTokens.Add(tokens[i] as AbstractSyntax);
                    else if (!(tokens[i] is CommaToken))
                        throw new SyntaxException("Invalid value in array: " + tokens[i]);
                }
                else if (braceCount > 1)
                {
                    subtreeTokens.Add(tokens[i]);
                }
                else
                    syntaxList.Add(tokens[i]);
            }

            // Numeracy B(IDMAS)
            // Assignments come first too.
            List<Type> numeracyOperatorTypes = new List<Type>()
            {
                typeof(PowerOperator),
                typeof(DivideOperator),
                typeof(MultiplyOperator),
                typeof(AddOperator),
                typeof(SubtractOperator),

                typeof(AssignmentOperator),
            };

            numeracyOperatorTypes.Reverse();

            List<object> rightSyntax;
            
            foreach (Type numeracyOperatorType in numeracyOperatorTypes)
            {
                rightSyntax = new List<object>();
                for (int i = syntaxList.Count - 1; i >= 0; i--)
                {
                    if (syntaxList[i].GetType() == numeracyOperatorType)
                    {
                        AbstractSyntax right;
                        AbstractSyntax left;
                        if (rightSyntax.Count < 1)
                            throw new SyntaxException("No right operand provided for " + numeracyOperatorType);
                        if (rightSyntax.Count == 1)
                        {
                            if (!(rightSyntax.First() is AbstractSyntax))
                                throw new SyntaxException($"Invalid right operand provided for {numeracyOperatorType}, {rightSyntax.First()}");

                            right = rightSyntax.First() as AbstractSyntax;
                        }
                        else
                        {
                            rightSyntax.Reverse();
                            right = new AbstractSyntaxTree(rightSyntax);
                        }

                        if (syntaxList.Count == 3 || i == 1)
                        {
                            if (!(syntaxList[i - 1] is AbstractSyntax))
                                throw new SyntaxException($"Invalid left operand provided for {numeracyOperatorType}, {syntaxList[i - 1]}");

                            left = syntaxList[i - 1] as AbstractSyntax;
                        }
                        else
                            left = new AbstractSyntaxTree(syntaxList.GetRange(0, i));

                        root = new BinaryOperationSyntax<AbstractSyntax, AbstractSyntax>((AbstractBinaryOperator)syntaxList[i], left, right);
                        return;
                    }
                    rightSyntax.Add(syntaxList[i]);
                }
            }

            // Remaining operations can be stacked without brackets.
            // They are parsed from the right down.
            rightSyntax = new List<object>();
            for (int i = syntaxList.Count - 1; i >= 0; i--)
            {
                if (syntaxList[i] is AbstractBinaryOperator)
                {
                    AbstractSyntax right;
                    AbstractSyntax left;
                    if (rightSyntax.Count < 1)
                        throw new SyntaxException("No right operand provided for " + syntaxList[i].GetType());
                    if (rightSyntax.Count == 1)
                    {
                        if (!(rightSyntax.First() is AbstractSyntax))
                            throw new SyntaxException($"Invalid right operand provided for {syntaxList[i].GetType()}, {rightSyntax.First()}");

                        right = rightSyntax.First() as AbstractSyntax;
                    }
                    else
                    {
                        rightSyntax.Reverse();
                        right = new AbstractSyntaxTree(rightSyntax);
                    }

                    if (syntaxList.Count == 3 || i == 1)
                    {
                        if (!(syntaxList[i - 1] is AbstractSyntax))
                            throw new SyntaxException($"Invalid left operand provided for {syntaxList[i].GetType()}, {syntaxList[i - 1]}");

                        left = syntaxList[i - 1] as AbstractSyntax;
                    }
                    else
                        left = new AbstractSyntaxTree(syntaxList.GetRange(0, i));


                    root = new BinaryOperationSyntax<AbstractSyntax, AbstractSyntax>((AbstractBinaryOperator)syntaxList[i], left, right);
                    return;
                }

                if (syntaxList[i] is AbstractUnaryOperator)
                {
                    AbstractSyntax right;
                    if (rightSyntax.Count < 1)
                        throw new SyntaxException("No right operand provided for " + syntaxList[i].GetType());
                    if (rightSyntax.Count == 1)
                    {
                        if (!(rightSyntax.First() is AbstractSyntax))
                            throw new SyntaxException($"Invalid right operand provided for {syntaxList[i].GetType()}, {rightSyntax.First()}");

                        right = rightSyntax.First() as AbstractSyntax;
                    }
                    else
                    {
                        rightSyntax.Reverse();
                        right = new AbstractSyntaxTree(rightSyntax);
                    }

                    root = new UnaryOperationSyntax<AbstractSyntax>((AbstractUnaryOperator)syntaxList[i], right);
                    return;
                }

                rightSyntax.Add(syntaxList[i]);
            }

            if (root == null && syntaxList.Count > 0)
                root = syntaxList.First() as AbstractSyntax;
        }

        public override object GetValue()
        {
            return root.GetValue();
        }
    }
}
