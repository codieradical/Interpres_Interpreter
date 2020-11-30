using Interpres.Tokens;
using Interpres.Tokens.Expressions;
using Interpres_dev;
using System;

class Evaluator
 {
     private readonly AbstractToken root;

     public Evaluator(AbstractToken root)
     {
         this.root = root;
     }

     public object Evaluate()
     {
        Console.WriteLine(root);
        return ExpressionEvaluator(root);
     }

    private ValueToken ExpressionEvaluator(AbstractToken node)
    {
        // Expression
        // Number Expression

        if (node is ValueToken n)
        {
            return n;
        }

        if (node is BinaryExpression e)
        {
            var left = ExpressionEvaluator(e.LeftOperand);
            var right = ExpressionEvaluator(e.RightOperand);

            return e.Operation.Operate(new ValueToken[] { left, right });
        }

        //if (node is Expression b)
        //    return ExpressionEvaluator(b);

        throw new Exception($"Unexpected node {node.GetType()}");
    }
 }