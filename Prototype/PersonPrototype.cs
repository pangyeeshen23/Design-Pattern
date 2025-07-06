using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Prototype
{
    public class Person
    {
        public string[] Names { get; set; }
        private Address Address;

        public Person(string[] names, Address address)
        {
            if(names == null) throw new ArgumentNullException(paramName: nameof(names));
            if(address == null) throw new ArgumentNullException(paramName: nameof(address));
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)} : {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int hourNum)
        {
            if (streetName == null) throw new ArgumentNullException(paramName: nameof(StreetName));
            if (hourNum == null) throw new ArgumentNullException(paramName: nameof(hourNum));
            StreetName = streetName;
            HouseNumber = hourNum;
        }

    }

    class PersonPrototype
    {
    }
}
