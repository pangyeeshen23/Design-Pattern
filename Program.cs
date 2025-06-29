using System.Text;
using DesignPattern.Builder;
using DesignPattern.Solid;
using static DesignPattern.Builder.FunctionalBuilder;
using static DesignPattern.Builder.StepwiseBuilder;
using static DesignPattern.Program;
using static DesignPattern.Solid.OpenClose;

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

            //Builder
            //HtmlBuilder builder = new HtmlBuilder("ul");
            //builder.AddChild("li", "hello");
            //builder.AddChild("li", "world");
            //Console.WriteLine(builder.ToString());

            // Fluent Builder
            //HtmlBuilder builder = new HtmlBuilder("ul");
            //builder.AddChild("li", "hello").AddChild("li", "world");
            //Console.WriteLine(builder.ToString());

            //Fluent Builder with inheritance and generic type
            //Person persone = Person.New
            //    .SetName("John")
            //    .WorkAsA("Software Engineer")
            //    .Earn(salary: 100000)
            //    .Build();
            //Console.WriteLine(persone.ToString());

            // Stepwise Builder
            //Car car = CarBuilder.Create() // returns - ISpeficyCarType
            //    .OfType(CarType.Crossover) // returns - ISpecifyWheelSize
            //    .WithWheels(20) // returns - IBuildCar
            //    .Build();
            //Console.WriteLine(car.ToString());

            // Functional Builder
            //FunctionalBuilder.Person person = new PersonBuilder()
            //    .Called("John")
            //    .WorkAs("Developer")
            //    .Build();
            //Console.WriteLine(person.ToString());

            // Farcade Builder
            //FarcadeBuilder.PersonBuilder personBuilder = new FarcadeBuilder.PersonBuilder();
            //FarcadeBuilder.Person person = personBuilder.Works.At("Microsoft")
            //    .AsA("Software Engineer")
            //    .Earning(100000)
            //    .Lives.At("123 St")
            //    .InCity("New York")
            //    .WithPostCode(10001)
            //    .Build();
            //Console.WriteLine(person.ToString());

            CodeBuilder cb = new CodeBuilder("Person")
                .AddField("Name", "string")
                .AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
