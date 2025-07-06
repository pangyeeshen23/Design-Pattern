using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Solid.OpenClose;

namespace DesignPattern.Solid
{
    /// <summary>
    /// Open Closed Principle (OCP) states that software entities (classes, modules, functions, etc.) 
    /// should be open for extension but closed for modification.
    /// meaning that the behavior of a class should be modified by adding new code, but to add new class to extend the
    /// behavior of existing class,
    /// </summary>
    static class OpenClose
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string Name;
            public Color Color;
            public Size Size;
            public Product(string name, Color color, Size size)
            {
                if (name == null) throw new ArgumentNullException(paramName: nameof(name));
                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> product, Size size)
            {
                foreach (var p in product)
                {
                    if (p.Size == size) yield return p;
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> product, Color color)
            {
                foreach (var p in product)
                {
                    if (p.Color == color) yield return p;
                }
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T item);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
        }

        public class ColorSepcification : ISpecification<Product>
        {
            private readonly Color _color;
            public ColorSepcification(Color color)
            {
                _color = color;
            }
            public bool IsSatisfied(Product item)
            {
                return item.Color == _color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private readonly Size _size;
            public SizeSpecification(Size size)
            {
                _size = size;
            }
            public bool IsSatisfied(Product item)
            {
                return item.Size == _size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            ISpecification<T> _first, _second;
            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                if (first == null) throw new ArgumentNullException(paramName: nameof(first));
                _first = first;
                if (second == null) throw new ArgumentNullException(paramName: nameof(second));
                _second = second;

            }

            public bool IsSatisfied(T t)
            {
                return _first.IsSatisfied(t) && _second.IsSatisfied(t);
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
            {
                foreach (var i in items)
                {
                    if (specification.IsSatisfied(i)) yield return i;
                }
            }
        }
    }

    public static class OpenClosedPrinciple
    {
        public static void MainProcess()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green Product (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is {p.Color}");
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (new): ");
            foreach (var p in bf.Filter(products, new ColorSepcification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is {p.Color}");
            }

            Console.WriteLine("Blue products (new): ");
            foreach (var p in bf.Filter(products, new AndSpecification<Product>(
                new ColorSepcification(Color.Blue), new SizeSpecification(Size.Large)
            )
            ))
            {
                Console.WriteLine($" - {p.Name} is blue and large ");
            }
        }
    }
}
