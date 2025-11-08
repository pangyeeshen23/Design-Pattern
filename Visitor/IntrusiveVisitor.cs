using ImpromptuInterface;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Visitor
{
    public static class IntrusiveVisitor
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
            public Expression Left { get; set;  }
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
            public static void Print(Expression e, StringBuilder sb)
            {
                if (e is DoubleExpression de)
                {
                    sb.Append(de.Value);
                }
                else if (e is AdditionExpression addItion)
                {
                    sb.Append("(");
                    Print(addItion.Left, sb);
                    sb.Append("+");
                    Print(addItion.Right, sb);
                    sb.Append(")");
                }
            }
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
