using MoreLinq;

namespace DesignPattern.Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount; // 0 by default
        public static int Count => instanceCount;

        public static SingletonDatabase Instance { get; } = 
            new Lazy<SingletonDatabase>(() => new SingletonDatabase()).Value;

        private SingletonDatabase()
        {
            instanceCount++;
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(),"Singleton", "capitals.txt");
            Console.WriteLine("Initializing Database");
            capitals = File.ReadAllLines(fullPath).Batch(2).ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1).Trim())
            );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        public static OrdinaryDatabase Instance { get; } =
            new Lazy<OrdinaryDatabase>(() => new OrdinaryDatabase()).Value;

        public OrdinaryDatabase()
        {
            instanceCount++;
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Singleton", "capitals.txt");
            Console.WriteLine("Initializing Database");
            capitals = File.ReadAllLines(fullPath).Batch(2).ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1).Trim())
            );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }


    public class DummyDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount; // 0 by default
        public static int Count => instanceCount;

        public static DummyDatabase Instance { get; } =
            new Lazy<DummyDatabase>(() => new DummyDatabase()).Value;

        private DummyDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initializing Dummy Database");
            capitals = new Dictionary<string, int>
            {
                { "Alpha", 1 },
                { "Beta", 2 },
                { "Gamma", 3 }
            };
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

    }


}
