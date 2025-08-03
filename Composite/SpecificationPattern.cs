using System.Drawing;

namespace DesignPattern.Composite
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, Specification<T> spec);

    }

    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T item);
        public static Specification<T> operator &(
            Specification<T> first, Specification<T> second
        )
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public abstract class CompositeSpecification<T> : Specification<T>
    {
        protected readonly Specification<T>[] items;
        protected CompositeSpecification(params Specification<T>[] items)
        {
            this.items = items;
        }
    }

    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public override bool IsSatisfied(T item)
        {
            return items.All(i => i.IsSatisfied(item));
        }

        public AndSpecification(params Specification<T>[] items) : base(items)
        {
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public override bool IsSatisfied(T item)
        {
            return items.Any(i => i.IsSatisfied(item));
        }

        public OrSpecification(params Specification<T>[] items) : base(items)
        {
            
        }
    }

    public class ColorSpecification : Specification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public override bool IsSatisfied(Product item)
        {
            return item.Color == color;
        }
    }


    public class Product
    {
        public Color Color { get; set; }
    }

    public class SpecificationPattern
    {


    }
}



