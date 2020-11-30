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

namespace Interpreter
{
    class AbstractSyntaxTree : AbstractSyntax
    {
        private AbstractSyntax root;
        
        public AbstractSyntaxTree(List<object> tokens)
        {
            BuildTree(tokens);
        }

        private Matrix ParseMatrix(List<object> tokens)
        {
            List<AbstractSyntax> contents = new List<AbstractSyntax>();
            return new Matrix(contents);
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
            List<object> subtreeTokens = new List<object>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is LeftParenthesisToken)
                    braceCount += 1;
                else if (tokens[i] is RightParenthesisToken)
                {
                    braceCount -= 1;
                    if (braceCount == 0)
                    {
                        if (i < tokens.Count - 1 || syntaxList.Count > 0)
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
                    syntaxList[i] = new IdentifierSyntax((IdentifierToken)syntaxList[i]);
            }

            if (syntaxList.Count == 1 && syntaxList.First() is ValueSyntax)
            {
                root = syntaxList.First() as ValueSyntax;
                return;
            }

            // 3. Parse Operations.
            // Indices B(I)DMAS
            // These are easy as they're expected to be in a simple format.
            for (int i = 0; i < syntaxList.Count; i++)
            {
                if (syntaxList[i] is PowerOperator && syntaxList[i + 1] is AbstractSyntax && syntaxList[i - 1] is AbstractSyntax)
                    syntaxList[i] = new BinaryOperationSyntax<AbstractSyntax, AbstractSyntax>((AbstractOperator)syntaxList[i], (AbstractSyntax)syntaxList[i - 1], (AbstractSyntax)syntaxList[i + 1]);
            }

            // Numeracy BI(DMAS)
            // Assignments come first too.
            List<Type> numeracyOperatorTypes = new List<Type>()
            {
                typeof(AssignmentOperator),
                
                typeof(DivideOperator),
                typeof(MultiplyOperator),
                typeof(AddOperator),
                typeof(SubtractOperator),
            };

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
                            right = new AbstractSyntaxTree(rightSyntax);

                        if (syntaxList.Count == 3 || i == 1)
                        {
                            if (!(syntaxList[i - 1] is AbstractSyntax))
                                throw new SyntaxException($"Invalid left operand provided for {numeracyOperatorType}, {syntaxList[i - 1]}");

                            left = syntaxList[i - 1] as AbstractSyntax;
                        }
                        else
                        {
                            left = new AbstractSyntaxTree(syntaxList.GetRange(0, i - 1));
                        }


                        root = new BinaryOperationSyntax<AbstractSyntax, AbstractSyntax>((AbstractOperator)syntaxList[i], left, right);
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
                if (syntaxList[i] is AbstractOperator)
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
                        right = new AbstractSyntaxTree(rightSyntax);

                    if (syntaxList.Count == 3 || i == 1)
                    {
                        if (!(syntaxList[i - 1] is AbstractSyntax))
                            throw new SyntaxException($"Invalid left operand provided for {syntaxList[i].GetType()}, {syntaxList[i - 1]}");

                        left = syntaxList[i - 1] as AbstractSyntax;
                    }
                    else
                        left = new AbstractSyntaxTree(syntaxList.GetRange(0, i - 1));


                    root = new BinaryOperationSyntax<AbstractSyntax, AbstractSyntax>((AbstractOperator)syntaxList[i], left, right);
                    return;
                }
                rightSyntax.Add(syntaxList[i]);
            }
        }

        public override object GetValue()
        {
            return root.GetValue();
        }
    }
}
