using DesignPattern.Singleton;

namespace DesignPattern
{
    public class Singletons
    {
        public void Run()
        {
            SingletonDatabase db = SingletonDatabase.Instance.Value;
            int population = db.GetPopulation("Tokyo");
            Console.WriteLine(population);
        }
    }


}
