

using System.Threading.Tasks;
using DesignPattern.Factory;
using static DesignPattern.Factory.Point;

namespace DesignPattern
{
    class Factories
    {
        public void MainProcess()
        {
            // Factory Method Pattern - When a class has class to instantiate and private initiatize constructor
            // then it is a Factory Method Pattern.
            //Point p1 = Point.NewCartesianPoint(10, 20);
            //Console.WriteLine(p1.ToString());
            //Point p2 = Point.NewPolarPoint(1.0, Math.PI / 2);
            //Console.WriteLine(p2.ToString());

            // Asynchronous Factory Method
            //Page page = await Page.CreateAsync();

            // Dedicated Factory Class with Factory Pattern
            //Point p1 = PointFactory.NewCartesianPoint(10, 20);
            //Point p2 = PointFactory.NewPolarPoint(10, 20);

            // Object Tracking and Bulk Replacement
            //TrackingThemeFactory factory = new TrackingThemeFactory();
            //ITheme theme1 = factory.CreateTheme(false);
            //ITheme theme2 = factory.CreateTheme(true);
            //Console.WriteLine(factory.Info);

            //ReplaceableThemeFactory replaceThemeFactory = new ReplaceableThemeFactory();
            //Ref<ITheme> magicTheme = replaceThemeFactory.CreateTheme(true);
            //Ref<ITheme> magicTheme2 = replaceThemeFactory.CreateTheme(false);
            //Console.WriteLine(magicTheme.Value.BgrColor);
            //replaceThemeFactory.ReplaceTheme(false); // note : to me not suitable to factory pattern. cause factory should instantiate object based on inputs only
            //Console.WriteLine(magicTheme.Value.BgrColor);

            // Inner Factory - for private constructor
            //Point p1 = Point.Factory.NewCartesianPoint(10, 20);
            //Console.WriteLine(p1);
            //Point p2 = Point.Factory.NewPolarPoint(10, 20);
            //Console.WriteLine(p2);
            //Point origin = Point.Origin;

            //Abstract Factory
            //HotDrinkMachine machine = new HotDrinkMachine();
            //IHotDrink drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 200);
            //drink.Consume();

            //Abstract Factory With Open Closed Principle By Using Assembly and Reflection
            //HotDrinkMachine machine = new HotDrinkMachine();
            //IHotDrink drink = machine.MakeDrink();
            //drink.Consume();

            Person.Factory personFactory = new Person.Factory();
            Person person = personFactory.CreatePerson("John");
            Person person2 = personFactory.CreatePerson("John");
            Console.WriteLine(person);
            Console.WriteLine(person2);
        }
    }
}
