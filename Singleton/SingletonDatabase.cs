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


        public static Lazy<SingletonDatabase> Instance { get; } = 
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());

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
}
