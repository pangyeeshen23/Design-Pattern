namespace DesignPattern.Decorator
{

    public interface IFly
    {
        void Fly();
    }

    public interface IWeight
    {
        int Weight { get; set; }

        public void GetWeight()
        {
            Console.WriteLine("Weight : " + Weight);
        }
    }

    public interface ICrawl
    {
        void Crawl();
    }

    public class Bird : IFly, IWeight
    {
        public int Weight { get; set; } = 10;

        public void Fly()
        {
            Console.WriteLine("Soaring in the sky");
        }
    }

    public class Lizard : ICrawl, IWeight
    {
        public int Weight { get; set; } = 20;
        public void Crawl()
        {
            Console.WriteLine("Crawling in the dirt");
        }
    }


    // This is a decorator pattern because we still use back the original class of bird and lizard
    public class Dragon : IFly, ICrawl, IWeight
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        public int Weight { get =>  bird.Weight + lizard.Weight; set { } }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
}
