using System.Diagnostics;

namespace DesignPattern.Proxy
{


    [DebuggerDisplay("{value*100.0f")]
    public struct Percentage
    {
        private readonly float value;
        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float left, Percentage right)
        {
            return left * right.value;
        }

        public static Percentage operator *(Percentage left, Percentage right)
        {
            return new Percentage(left.value * right.value);
        }

        public static Percentage operator +(Percentage left, Percentage right)
        {
            return new Percentage(left.value + right.value);
        }

        public override string ToString()
        {
            return $"{this.value * 100}%";
        }
    }


    public static class PercentageExtension
    {
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100f);
        }

        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100f);
        }
    }

    public class ValueProxy
    {
        public void Run()
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(2.Percent() * 5.Percent());
        }
    }
}
