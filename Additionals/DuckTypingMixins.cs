using System.Collections;

namespace DesignPattern.Additionals
{
    public class Foo
    {

    }

    interface IScalar<T> : IEnumerable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return (T) this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    interface IMyDisposable<T> : IDisposable
    {
        void IDisposable.Dispose()
        {
            Console.WriteLine($"Disposing {typeof(T).Name}");
        }
    }

    public class MyClass : IScalar<MyClass>
    {
        public override string ToString()
        {
            return "MyClass";
        }
    }

    public class MyClassTwo : IMyDisposable<MyClass>
    {
        public override string ToString()
        {
            return "MyClass";
        }
    }

    public class Bar : Foo
    {

    }

    //ref struct Foo
    //{
    //    public void Dispose()
    //    {
    //        Console.WriteLine("Disposing Foo");
    //    }
    //}

    public static class DuckTypingMixins
    {
        public static void Run()
        {
            var myClass = new MyClass();
            foreach (var item in myClass)
            {
                Console.WriteLine(item);
            }


        }
    }
}
