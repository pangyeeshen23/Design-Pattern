namespace DesignPattern.Decorator
{

    // Default interface members
    public interface ICreature
    {
        int Age { get; set; }

    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if(Age > 10)
                Console.WriteLine("I am flying");
        }
    }

    public interface  ILizard : ICreature
    {
        void Crawl()
        {
            if(Age < 10)
                Console.WriteLine("I am crawling");
        }
    }

    public class Organism
    {

    }

    public class DragonTwo : Organism, IBird, ILizard
    {
        public int Age { get; set; }
    }


    // inheritance
    // Smart Dragon(Dragon)
    // extension method
    // C#8 default interface methods


}
