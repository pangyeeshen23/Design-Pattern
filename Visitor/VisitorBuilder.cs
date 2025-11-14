using DesignPattern.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Visitor
{
    public class VisitorBuilders
    {

        public abstract class Expression;

        public class DoubleExpression(double value) : Expression
        {
            public readonly double Value = value;
        }

        public class AdditionExpression(Expression left, Expression right) : Expression
        {
            public readonly Expression Left = left, Right = right;
        }

        public class MultiplicationExpression(Expression left, Expression right) : AdditionExpression(left, right);

        public interface IVisitor<T, TResult>
        {
            TResult Visit(IVisitor<T, TResult> visitor, T node);
            TResult Visit(T node);
        }

        public class VisitorBuilder<T, TResult> 
        {
            public static VisitorBuilder<T, TResult> New = new();
            private readonly Dictionary<Type, Func<IVisitor<T, TResult>, T, TResult>> visitors = new ();
            private Func<IVisitor<T, TResult>, T, TResult> defaultVisitor;

            public VisitorBuilder<T, TResult> For<TNode>(Func<IVisitor<T, TResult>, TNode, TResult> visitor) 
                where TNode : T
            {
                visitors[typeof(TNode)] = (v, node) => visitor(v, (TNode)node);
                return this;
            }

            public VisitorBuilder<T, TResult> Default(Func<IVisitor<T, TResult>, T, TResult> visitor)
            {
                defaultVisitor = visitor;
                return this;
            }

            public IVisitor<T, TResult> Build() => new BuiltVisitor(visitors, defaultVisitor);

            private class BuiltVisitor : IVisitor<T, TResult>
            {
                private readonly Dictionary<Type, Func<IVisitor<T, TResult>, T, TResult>> visitors = new();
                private readonly Func<IVisitor<T, TResult>, T, TResult> _defaultVisitor;

                public BuiltVisitor(Dictionary<Type, Func<IVisitor<T, TResult>, T, TResult>> visitors, Func<IVisitor<T, TResult>, T, TResult> defaultVisitor)
                {
                    this.visitors = visitors;
                    this._defaultVisitor = defaultVisitor;
                }

                public TResult Visit(IVisitor<T, TResult> self, T node)
                {
                    var type = node.GetType();
                    if (visitors.TryGetValue(type, out var visitor))
                    {
                        return visitor(self, node);
                    }
                    return _defaultVisitor(self, node);
                }

                public TResult Visit(T node) => Visit(this, node);
            }
        }

        public static void Run()
        {
            AdditionExpression expression = new AdditionExpression(new DoubleExpression(5), new MultiplicationExpression(new DoubleExpression(3), new DoubleExpression(2)));

            var printer = VisitorBuilder<Expression, string>.New
                .For<DoubleExpression>((_, de) => de.Value.ToString())
                .For<AdditionExpression>((v, ae) => $"({v.Visit(v, ae.Left)}) + ({v.Visit(v, ae.Right)})")
                .For<MultiplicationExpression>((v, ae) => $"({v.Visit(v, ae.Left)}) * ({v.Visit(v, ae.Right)})")
                .Build();

            var evaluator = VisitorBuilder<Expression, double>.New
                .For<DoubleExpression>((_, de) => de.Value)
                .For<AdditionExpression>((v, ae) => v.Visit(v, ae.Left) + v.Visit(v, ae.Right))
                .For<MultiplicationExpression>((v, ae) => v.Visit(v, ae.Left) * v.Visit(v, ae.Right))
                .Build();

            var print = printer.Visit(expression);
            var value = evaluator.Visit(expression);
            Console.WriteLine($"{print} = {value}");
        }
    }
}
