using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Composite
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);

    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
        public abstract bool IsSatisfied(Product item);
    }
}

    public class ColorSpecification : ISpecification<Product>
    {

    }

    public class Product
    {

    }

    public class SpecificationPattern
    {
        

    }
}
