using DesignPattern.Singleton;

namespace DesignPattern
{
    public class Singletons
    {
        public void Run()
        {
            //SingletonDatabase db = SingletonDatabase.Instance;
            //int population = db.GetPopulation("Tokyo");
            //Console.WriteLine(population);

            //CEO ceo = new CEO();
            //ceo.Name = "Adam Smith";
            //ceo.Age = 50;

            //CEO ceo2 = new CEO();
            //Console.WriteLine(ceo2);

            //Task t1 = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
            //    Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
            //});

            //Task t2 = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
            //    Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
            //});
            //Task.WaitAll(t1, t2);

            Building building = new Building();
            // gnd 3000
            using (new BuildingContext(3000))
            {
                building.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                building.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
                building.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
            }

            // 1st 3500
            using (new BuildingContext(3500))
            {
                building.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
                building.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
            }

            Console.WriteLine(building);
        }
    }

    // this class uses the SingletonDatabase to find the total population of a list of city names.
    // but there is a shortcoming: it uses a singleton directly, which makes it hard to test or change the database implementation.
    // this is because the Instance has a hardcoded dependency on SingletonDatabase.
    // which in turn make it unable to use Mock or Stub for testing purposes.
    public class SingletonsRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    // This class is used to demonstrate dependency injection and allows for different database implementations.
    // which solve the shortcoming that had been mention above.
    public class ConfigurableRecordFinder
    {
        private IDatabase database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach(var name in names)
            {
                result += this.database.GetPopulation(name);
            }
            return result;
        }
    }
}
