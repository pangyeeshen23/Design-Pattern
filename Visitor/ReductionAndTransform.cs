using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Visitor.ReductionAndTransform;

namespace DesignPattern.Visitor
{
    public class ReductionAndTransform
    {
        public abstract class Expression
        {
            public abstract T Reduce<T>(ITransformer<T> transformer);
        }

        public interface ITransformer<out T>
        {
            T Transform(DoubleExpression de);
            T Transform(AdditionExpression ae);
        }


        public class DoubleExpression : Expression
        {
            public readonly double Value;

            public DoubleExpression(double value)
            {
                Value = value;
            }
            
            public override T Reduce<T>(ITransformer<T> transformer)
            {
                return transformer.Transform(this);
            }
        }

        public class AdditionExpression : Expression
        {
            public readonly Expression Left, Right;

            public AdditionExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }

            public override T Reduce<T>(ITransformer<T> transformer)
            {
                return transformer.Transform(this);
            }
        }


        public class EvaluationTranformer : ITransformer<double>
        {
            public double Transform(DoubleExpression de)
            {
                return de.Value;
            }

            public double Transform(AdditionExpression ae)
            {
                return ae.Left.Reduce(this) + ae.Right.Reduce(this);
            }
        }

        public class PrintTranformer : ITransformer<string>
        {
            public string Transform(DoubleExpression de)
            {
                return de.Value.ToString();
            }

            public string Transform(AdditionExpression ae)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("(");
                sb.Append(ae.Left.Reduce<string>(this));
                sb.Append("+");
                sb.Append(ae.Right.Reduce<string>(this));
                sb.Append(")");
                return sb.ToString();
            }
        }

        public class SquareTransformer : ITransformer<Expression>
        {
            public Expression Transform(DoubleExpression de)
            {
                return new DoubleExpression(de.Value * de.Value); 
            }

            public Expression Transform(AdditionExpression ae)
            {
                return new AdditionExpression(ae.Left.Reduce(this), ae.Right.Reduce(this));
            }
        }

        public static void Run()
        {
            AdditionExpression expr = new AdditionExpression(new DoubleExpression(1), new DoubleExpression(2));
            EvaluationTranformer et = new EvaluationTranformer();
            var result = expr.Reduce(et);
            Console.WriteLine(result);

            PrintTranformer pt = new PrintTranformer();
            string print = expr.Reduce(pt);
            Console.WriteLine(print);

            SquareTransformer st = new SquareTransformer();
            Expression newExpr = expr.Reduce(st);
            print = newExpr.Reduce(pt);
            Console.WriteLine(print);
        }
    }
}
