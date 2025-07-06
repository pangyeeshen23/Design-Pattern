using DesignPattern.Prototype;

namespace DesignPattern
{
    class Prototypes
    {
        public void Run()
        {
            Person person = new Person(new[] {"John", "Smith"}, new Address("London Road", 123));
            Console.WriteLine(person);
        }
    }
}
