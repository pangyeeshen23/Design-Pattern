using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Iterator
{
    public class Excersize
    {
        public class Node<T>
        {
            public T Value;
            public Node<T> Left, Right;
            public Node<T> Parent;

            public Node(T value)
            {
                Value = value;
            }

            public Node(T value, Node<T> left, Node<T> right)
            {
                Value = value;
                Left = left;
                Right = right;

                left.Parent = right.Parent = this;
            }

            public IEnumerable<T> PreOrder
            {
                get
                {
                    foreach (Node<T> node in PreOrderIterator(this))
                        yield return node.Value;
                }
            }

            private IEnumerable<Node<T>> PreOrderIterator(Node<T> current)
            {
                yield return current;
                if (current.Left != null)
                    foreach (var n in PreOrderIterator(current.Left))
                        yield return n;
                if (current.Right != null)
                    foreach (var n in PreOrderIterator(current.Right))
                        yield return n;
            }
        }

        public void Run()
        {
            var root = new Node<int>(1,
                        new Node<int>(2,
                            new Node<int>(4),
                            new Node<int>(5)),
                        new Node<int>(3,
                            new Node<int>(6),
                            new Node<int>(7)));
            foreach (var n in root.PreOrder)
                Console.WriteLine(n);
        }
    }
}
