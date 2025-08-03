using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Decorator
{
    // Adapter and Decorator Pattern
    public class MyStringBuilder
    {
        StringBuilder _stringBuilder = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb._stringBuilder.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb._stringBuilder.Append(s);
            return msb;
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }

    public class AdapterDecorator
    {
        public void Run()
        {
            MyStringBuilder s = "Hello";
            s += "World";
            Console.WriteLine(s);
        }
    }
}
