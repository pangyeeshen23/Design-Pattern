
namespace DesignPattern.Prototype
{
    public class RecursivePrototype
    {


        public class Address
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

        }

        public class Person
        {
            public string[] Names { get; set; }
            public Address Address { get; set; }

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
        }
    }

}
