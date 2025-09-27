using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq.Extensions;

namespace DesignPattern.Iterator
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

            Left.Parent = Right.Parent = this;
        }
    }


    // this is c++ way of implementing an iterator pattern
    public class InOrderIterator<T>
    {
        private readonly Node<T> root;
        public Node<T> Current { get; set; }
        private bool yieldedStart;
        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;
            while (Current.Left != null)
                Current = Current.Left;
        }

        public bool MoveNext()
        {
            if(!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }
            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var p = Current.Parent;
                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }
                Current = p;
                return Current != null;
            }
        }

        public void Reset()
        {
            Current = root;
            yieldedStart = false;
        }
    }

    public class BinaryTree<T>
    {
        private Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }

        public InOrderIterator<T> GetEnumerator()
        {
            return new InOrderIterator<T>(root);
        }
    }

    public class BasicIterator
    {
        public void Run()
        {
            var root = new Node<int>(1,
                new Node<int>(2, new Node<int>(4), new Node<int>(5)),
                new Node<int>(3, new Node<int>(6), new Node<int>(7)));
            //var it = new InOrderIterator<int>(root);
            //while (it.MoveNext())
            //{
            //    Console.Write(it.Current.Value);
            //    Console.Write(",");
            //}

            var tree = new BinaryTree<int>(root);
            //Console.WriteLine(string.Join(",", tree.InOrder.Select(x => x.Value)));

            foreach (var v in tree)
            {
                Console.WriteLine(v.Value);
            }
        }
    }
}
