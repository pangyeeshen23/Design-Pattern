using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but i prefer it with milk");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is sensational !");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    public class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in {amount} ml of water");
            Console.WriteLine("Add some tea leaves");
            return new Tea();
        }
    }

    public class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in {amount} ml of water");
            Console.WriteLine("Add some coffee grounds");
            return new Coffee();
        }
    }

    //public class HotDrinkMachine
    //{
    //public enum AvailableDrink
    //{
    //    Coffee,
    //    Tea
    //}

    //public Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

    //public HotDrinkMachine()
    //{
    //    foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
    //            Type.GetType("DesignPattern.Factory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"
    //        ));
    //        factories.Add(drink, factory);
    //    }
    //}

    //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    //{
    //    return factories[drink].Prepare(amount);
    //}
    //}

    public class HotDrinkMachine
    {
        public List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (Type type in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                // Get from assembly. But better ot use DI in normal setup
                if(typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
                {
                    factories.Add(
                        Tuple.Create(
                            type.Name.Replace("Factory", string.Empty),
                            (IHotDrinkFactory) Activator.CreateInstance(type)
                        )
                    );
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Availabel Drinks:");
            Console.WriteLine("Select drink by giving index: ");
            for (int index = 0; index < factories.Count; index++ )
            {
                var tuple = factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s = Console.ReadLine();
                if (s != null && int.TryParse(s, out int i) && i >= 0 && i < factories.Count)
                {
                    return factories[i].Item2.Prepare(200); // default amount
                }
                else
                {
                    Console.WriteLine("Please select a valid index.");
                }
            }
        }
    }

}
