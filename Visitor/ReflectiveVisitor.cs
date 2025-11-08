using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Visitor.ReflectiveVisitor;

namespace DesignPattern.Visitor
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;
    public static class ReflectiveVisitor
    {

        public abstract class Expression
        {
        }

        public class DoubleExpression : Expression
        {
            public double Value { get; set; }

            public DoubleExpression(double value)
            {
                Value = value;
            }

        }

        public class AdditionExpression : Expression
        {
            public Expression Left { get; set; }
            public Expression Right { get; set; }

            public AdditionExpression(Expression left, Expression right)
            {
                if (left == null) throw new ArgumentNullException(paramName: nameof(left));
                if (right == null) throw new ArgumentNullException(paramName: nameof(right));
                this.Left = left;
                this.Right = right;
            }

        }

        public static class ExpressionPrinter
        {
            private static DictType Actions = new DictType
            {
                [typeof(DoubleExpression)] = (e, sb) =>
                {
                    var de = (DoubleExpression)e;
                    sb.Append(de.Value);
                },
                [typeof(AdditionExpression)] = (e, sb) =>
                {
                    var addItion = (AdditionExpression)e;
                    sb.Append("(");
                    Print(addItion.Left, sb);
                    sb.Append("+");
                    Print(addItion.Right, sb);
                    sb.Append(")");
                }
            };

            public static void Print(Expression e, StringBuilder sb)
            {
                Actions[e.GetType()].Invoke(e, sb);
            }

            //public static void Print(Expression e, StringBuilder sb)
            //{
            //    if (e is DoubleExpression de)
            //    {
            //        sb.Append(de.Value);
            //    }
            //    else if (e is AdditionExpression addItion)
            //    {
            //        sb.Append("(");
            //        Print(addItion.Left, sb);
            //        sb.Append("+");
            //        Print(addItion.Right, sb);
            //        sb.Append(")");
            //    }
            //}
        }


        public static void Run()
        {
            Expression e = new AdditionExpression(new DoubleExpression(1), new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            StringBuilder sb = new StringBuilder();
            //e.Print(sb);
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
        }
    }
}
