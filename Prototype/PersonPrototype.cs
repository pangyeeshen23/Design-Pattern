using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Prototype
{

    class PersonPrototype
    {
        // IClonable interface is not the interface to be used to create a deep copy
        public class Person : ICloneable, IPrototype<Person>
        {
            public string[] Names { get; set; }
            public Address Address;

            public Person(string[] names, Address address)
            {
                if (names == null) throw new ArgumentNullException(paramName: nameof(names));
                if (address == null) throw new ArgumentNullException(paramName: nameof(address));
                Names = names;
                Address = address;
            }

            // Copy Constructor
            public Person(Person other)
            {
                Names = other.Names;
                Address = new Address(other.Address);
            }

            public override string ToString()
            {
                return $"{nameof(Names)} : {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }

            public object Clone()
            {
                return new Person(Names, (Address)Address.Clone());
            }

            public Person DeepCopy()
            {
                return new Person(Names, Address.DeepCopy());
            }
        }

        public class Address : ICloneable, IPrototype<Address>
        {
            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            // Copy Constructor
            public Address(Address otherAddress)
            {
                StreetName = otherAddress.StreetName;
                HouseNumber = otherAddress.HouseNumber;
            }

            public Address(string streetName, int hourNum)
            {
                if (streetName == null) throw new ArgumentNullException(paramName: nameof(StreetName));
                if (hourNum == null) throw new ArgumentNullException(paramName: nameof(hourNum));
                StreetName = streetName;
                HouseNumber = hourNum;
            }
            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }

            public object Clone()
            {
                return new Address(StreetName, HouseNumber);
            }

            public Address DeepCopy()
            {
                return new Address(StreetName, HouseNumber);
            }
        }

        public interface IPrototype<T>
        {
            T DeepCopy();
        }

    }
}
