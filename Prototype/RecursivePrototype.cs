
using static DesignPattern.Prototype.RecursivePrototype;

namespace DesignPattern.Prototype
{

    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new()
        {
            return item.DeepCopy();
        }
    }

    public class RecursivePrototype
    {
        public interface IDeepCopyable<T> where T : new()
        {
            void CopyTo(T target);

            public T DeepCopy()
            {
                T copy = new T();
                CopyTo(copy);
                return copy;
            }
        }


        public class Address : IDeepCopyable<Address>
        {
            public string StreetName { get; set; }
            public int HouseNumber { get; set; }
            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName;
                HouseNumber = houseNumber;
            }
            public Address()
            {

            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, " +
                    $"{nameof(HouseNumber)}: {HouseNumber}";
            }

            public Address DeepCopy()
            {
                return (Address) MemberwiseClone();
            }

            public void CopyTo(Address target)
            {
                target.StreetName = StreetName;
                target.HouseNumber = HouseNumber;
            }
        }

        public class Person : IDeepCopyable<Person>
        {
            public string[] Names { get; set; }
            public Address Address { get; set; }

            public Person()
            {
                
            }

            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(",", Names)}, " +
                    $"{nameof(Address)}: {Address}";
            }

            public Person DeepCopy()
            {
                return new Person((string[]) Names.Clone(), Address.DeepCopy());
            }

            public void CopyTo(Person target)
            {
                target.Names = (string[])Names.Clone();
                target.Address = Address.DeepCopy();
            }
        }

        public class Employee : Person, IDeepCopyable<Employee>
        {
            public int Salary { get; set; }
            public Employee()
            {

            }

            public Employee(string[] names, Address address, int salary) : base(names, address)
            {
                Salary = salary;
            }

            public override string ToString()
            {
                return $"{base.ToString()}, " +
                    $"{nameof(Salary)}: {Salary}";
            }

            public Employee Copy()
            {
                return new Employee(Names, Address, Salary);
            }

            public new Employee DeepCopy()
            {
                return new Employee((string[])Names.Clone(), Address.DeepCopy(), Salary);
            }

            public void CopyTo(Employee target)
            {
                base.CopyTo(target);
                target.Salary = Salary;
            }
        }
    }

}
