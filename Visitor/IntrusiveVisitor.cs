using System.Text;

namespace DesignPattern.Visitor
{
    public static class IntrusiveVisitor
    {
        public abstract class Expression
        {
            public abstract void Print(StringBuilder sb);
        }

        public class DoubleExpression : Expression
        {
            private double _value;

            public DoubleExpression(double value)
            {
                _value = value;
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append(_value);
            }
        }

        public class AdditionExpression : Expression
        {
            private Expression _left, _right;

            public AdditionExpression(Expression left, Expression right)
            {
                if (left == null) throw new ArgumentNullException(paramName: nameof(left));
                if (right == null) throw new ArgumentNullException(paramName: nameof(right));
                this._left = left;
                this._right = right;
            }

            public override void Print(StringBuilder sb)
            {
                sb.Append("(");
                _left.Print(sb);
                sb.Append('+');
                _right.Print(sb);
                sb.Append(")");

            }
        }

        public static void Run()
        {
            Expression e = new AdditionExpression(new DoubleExpression(1), new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            StringBuilder sb = new StringBuilder();
            e.Print(sb);
            Console.WriteLine(sb);
        }


    }
}
