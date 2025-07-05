namespace DesignPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SOLID Principles
            //SingleResponsibility.MainProcess();
            //OpenClosedPrinciple.MainProcess();
            //LiskovSubsitution.MainProcess();
            //InterfaceSegregation.MainProcess();
            //DependencyInversion.MainProcess();

            // Everything related to Builder Pattern
            //Builders builder = new Builders();
            //builder.MainProcess();

            Factories factory = new Factories();
            factory.MainProcess();
        }
    }
}
