using DesignPattern.Proxy;

namespace DesignPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SOLID Principles
            //SingleResponsibility.MainProcess();
            //OpenClosedPrinciple.MainProcess();
            //LiskovSubsitution.MainProcess();
            //InterfaceSegregation.MainProcess();
            //DependencyInversion.MainProcess();

            //Builder Pattern
            //Builders builder = new Builders();
            //builder.Run();

            //Factory Pattern
            //Factories factory = new Factories();
            //factory.Run();

            //Prototype Pattern
            //Prototypes prototypes = new Prototypes();
            //prototypes.Run();

            //Singleton Pattern
            //Singletons singletons = new Singletons();
            //singletons.Run();

            //Adapter Pattern
            //Adapters adapters = new Adapters();
            //adapters.Run();

            //Bridge Pattern
            //Bridges bridges = new Bridges();
            //bridges.Run();

            //Composition Pattern
            //Composites composites = new Composites();
            //composites.Run();

            //Decorator Pattern
            //Decorators decorators = new Decorators();
            //decorators.Run();

            //Farcade Pattern
            //Farcades farcade = new Farcades();
            //farcade.Run();

            //FlyWeights flyWeight = new FlyWeights();
            //flyWeight.Run();

            //Proxy Pattern
            //Proxies proxies = new Proxies();
            //proxies.Run();

            //Chain of Responsibility Pattern
            ChainOfResponsibility chain = new ChainOfResponsibility();
            chain.Run();

        }
    }
}
