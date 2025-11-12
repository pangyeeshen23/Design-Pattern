using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Visitor
{
    public class AcyclicVisitor
    {
        
        public interface IVisitor<TVisitable>
        {
            void Visit(TVisitable obj);
        }

        public interface IVisitor
        {

        }

        // 3 - Double Expression
        // (1 + 2) (1+(2+3))
        public abstract class Expression
        {
            public virtual void Accept(IVisitor visitor)
            {
                if (visitor is IVisitor<Expression> typed)
                    typed.Visit(this);
            }
        }


        public class DoubleExpression : Expression
        {
            public double Value;
            public DoubleExpression(double value)
            {
                Value = value;
            }

            public override void Accept(IVisitor visitor)
            {
                if(visitor is IVisitor<DoubleExpression> typed)
                    typed.Visit(this);
            }
        }

        public class AdditionExpression : Expression
        {
            public Expression Left, Right;

            public AdditionExpression(Expression left, Expression right)
            {
                Left = left ?? throw new ArgumentNullException(nameof(left));
                Right = right ?? throw new ArgumentNullException(nameof(right));
            }

            public override void Accept(IVisitor visitor)
            {
                if (visitor is IVisitor<AdditionExpression> typed)
                    typed.Visit(this);
            }
        }

        public class ExpressionPrinter : IVisitor, IVisitor<Expression>, IVisitor<DoubleExpression>, IVisitor<AdditionExpression>
        {

            private StringBuilder _sb = new StringBuilder();

            public void Visit(Expression obj)
            {
                
            }

            public void Visit(DoubleExpression obj)
            {
                _sb.Append(obj.Value);
            }

            public void Visit(AdditionExpression obj)
            {
                _sb.Append("(");
                obj.Left.Accept(this);
                _sb.Append("+");
                obj.Right.Accept(this);
                _sb.Append(")");
            }

            public override string ToString() => _sb.ToString();
        }

        public static void Run()
        {
            AdditionExpression add = new AdditionExpression(new DoubleExpression(1), new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            ExpressionPrinter printer = new ExpressionPrinter();
            printer.Visit(add);
            Console.WriteLine(printer.ToString());
        }
    }
}
