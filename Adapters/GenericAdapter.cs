namespace DesignPattern.Adapter
{
    public class Vector<TSelf, T, TDimension> 
        where TDimension : IInteger, new()
        where TSelf : Vector<TSelf, T, TDimension>, new()
    {
        protected T[] data;

        public Vector()
        {
            data = new T[new TDimension().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new TDimension().Value;
            data = new T[requiredSize];
            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                data[i] = values[i];
        }

        public static TSelf Create(params T[] values)
        {
            var result = new TSelf();
            var requiredSize = new TDimension().Value;
            result.data = new T[requiredSize];
            var providedSize = values.Length;
            for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
                result.data[i] = values[i];

            return result;
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public T X
        {
            get => data[0];
            set => data[0] = value;
        }
    }

    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimension
    {

        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D> 
        where D : IInteger, new ()
        where TSelf : Vector<TSelf, float, D>, new()
    {

    }

    public class VectorOfInt<TSelf, D> : Vector<TSelf, int, D> 
        where D : IInteger, new()
        where TSelf : Vector<TSelf, int, D>, new()
    {
        public VectorOfInt() : base()
        {

        }
        public VectorOfInt(params int[] values) : base(values)
        {

        }

        public static VectorOfInt<TSelf, D> operator + 
            (VectorOfInt<TSelf, D> lhs, VectorOfInt<TSelf, D> rhs)
        {
            var result = new VectorOfInt<TSelf, D>();
            var dim = new D().Value;
            for (int i = 0; i < dim; i ++)
            {
                result[i] = lhs[i] + rhs[i];
            }
            return result;
        }
    }

    public class Vector2i : VectorOfInt<Vector2i, Dimension.Two>
    {
        public Vector2i()
        {
            
        }

        public Vector2i(params int[] values) : base(values)
        {
            
        }
    }

    public class Vector3f : VectorOfFloat<Vector3f, Dimension.Three>
    {
        public override string ToString()
        {
            return $"{string.Join(",", data)}";
        }
    }

    class GenericAdapter
    {
        public void Run()
        {
            Vector2i v = new Vector2i(1, 2);
            v[0] = 0;
            Vector2i vv = new Vector2i(3, 2);
            vv[0] = 0;
            var result = v + vv;

            Vector3f u = Vector3f.Create(1.0f, 2.0f, 3.0f);
        }
    }
}
