using DesignPattern.Prototype;

namespace DesignPattern
{
    class Prototypes
    {
        public void Run()
        {
            //PersonPrototype.Person john = new PersonPrototype.Person(new[] {"John", "Smith"}, new PersonPrototype.Address("London Road", 123));
            //PersonPrototype.Person jane = john.DeepCopy();
            //jane.Address.HouseNumber = 321;
            //Console.WriteLine(john);
            //Console.WriteLine(jane);

            RecursivePrototype.Employee john = new RecursivePrototype.Employee(
                new[] { "John", "Smith" }, 
                new RecursivePrototype.Address("London Road", 123),
                321000
            );
            RecursivePrototype.Employee jane = john.DeepCopy();
            jane.Address.HouseNumber = 150;
            Console.WriteLine(jane);
            Console.WriteLine(john);

            RecursivePrototype.Employee employee = john.DeepCopy<RecursivePrototype.Employee>();
            Console.WriteLine(employee);

            RecursivePrototype.Person person = john.DeepCopy<RecursivePrototype.Person>();
            Console.WriteLine(person);


            RecursivePrototype.Employee johnathan = john.Copy();
            johnathan.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(johnathan);
        }
    }
}
