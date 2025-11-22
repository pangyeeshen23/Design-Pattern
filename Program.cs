using DesignPattern.Additionals;
using DesignPattern.Command;
using DesignPattern.Interpreter;
using DesignPattern.Observer;
using DesignPattern.Proxy;
using static DesignPattern.Additionals.ContinuationPassing;

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
            //ChainOfResponsibility chain = new ChainOfResponsibility();
            //chain.Run();

            //Commands command = new Commands();
            //command.Run();

            //Interpretors interpreters = new Interpretors();
            //interpreters.Run();

            //Iterators iterators = new Iterators();
            //iterators.Run();

            //Mediatorss mediators = new Mediatorss();
            //mediators.Run();

            //Mementos mementos = new Mementos();
            //mementos.Run();

            //NullObjects no = new NullObjects();
            //no.Run();

            //Observers observer = new Observers();
            //observer.Run();

            //States.Run();

            //Strategies.Run();

            //Templates.Run();

            //Visitors.Run();

            //DuckTypingMixins.Run();

            //ASIISTR.Run();

            //ContinuationPassingStyleDemo.Run();

            //DuckTypingMixins.Run();

            CQRS.Execute();
        }
    }
}
