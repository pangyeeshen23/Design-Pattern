using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Visitor
{
    public class DynamicVisitor
    {
        public abstract class Expression
        {
        }

        public class DoubleExpression : Expression
        {
            public double Value;
            public DoubleExpression(double value)
            {
                this.Value = value;
            }
        }

        public class AdditionExpression : Expression
        {
            public Expression Left, Right;
            public AdditionExpression(Expression left, Expression right)
            {
                Left = left;
                Right = right;
            }


        }

        public class ExpressionPrinter
        {
            public void Print(AdditionExpression ae, StringBuilder sb)
            {
                sb.Append("(");
                Print((dynamic)ae.Left, sb);
                sb.Append("+");
                Print((dynamic)ae.Right, sb);
                sb.Append(")");
            }

            public void Print(DoubleExpression de, StringBuilder sb)
            {
                sb.Append(de.Value);
            }
        }

        public static void Run()
        {
            AdditionExpression ae = new AdditionExpression(new DoubleExpression(1), new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            ExpressionPrinter ep = new ExpressionPrinter();
            StringBuilder sb = new StringBuilder();
            ep.Print(ae, sb);
            Console.WriteLine(sb.ToString());
        }
    }

    
}
