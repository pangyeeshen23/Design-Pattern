using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Visitor.ClassicVisitor;

namespace DesignPattern.Visitor
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

    public class ClassicVisitor
    {
        public interface IExpressionVisitor
        {
            void Visit(DoubleExpression de);
            void Visit(AdditionExpression ae);
        }

        public abstract class Expression
        {
            public abstract void Accept(IExpressionVisitor visitor);
        }

        public class DoubleExpression : Expression
        {
            public double Value { get; set; }

            public DoubleExpression(double value)
            {
                Value = value;
            }

            public override void Accept(IExpressionVisitor visitor)
            {
                // double dispatch
                visitor.Visit(this);
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

            public override void Accept(IExpressionVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public class ExpressionPrinter : IExpressionVisitor
        {
            StringBuilder sb = new StringBuilder();

            public void Visit(DoubleExpression de)
            {
                sb.Append(de.Value);
            }

            public void Visit(AdditionExpression ae)
            {
                sb.Append("(");
                ae.Left.Accept(this);
                sb.Append("+");
                ae.Right.Accept(this);
                sb.Append(")");
            }

            public override string ToString()
            {
                return sb.ToString();
            }
        }

        public class ExpressionCalculator : IExpressionVisitor
        {
            public double Result;

            public void Visit(DoubleExpression de)
            {
                Result = de.Value;
            }

            public void Visit(AdditionExpression ae)
            {
                ae.Left.Accept(this);
                var a = Result;
                ae.Right.Accept(this);
                var b = Result;
                Result = a + b;
            }
        }


        public static void Run()
        {
            AdditionExpression e = new AdditionExpression(new DoubleExpression(1), new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            ExpressionPrinter ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep);
            ExpressionCalculator calc = new ExpressionCalculator();
            calc.Visit(e);
            Console.WriteLine($"{ep} = {calc.Result}");
        }

    }
}
