using DesignPattern.Prototype;

namespace DesignPattern
{
    class Prototypes
    {
        public void Run()
        {
            PersonPrototype.Person john = new PersonPrototype.Person(new[] {"John", "Smith"}, new PersonPrototype.Address("London Road", 123));
            PersonPrototype.Person jane = john.DeepCopy();
            jane.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
