using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Builder;

namespace DesignPattern
{
    class Builders
    {
        public void Run()
        {
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
