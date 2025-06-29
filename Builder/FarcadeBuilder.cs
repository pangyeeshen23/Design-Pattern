using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    public class FarcadeBuilder
    {
        public class Person
        {
            public string StreetAddress { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }

            // employment
            public string CompanyName { get; set; }
            public string Position { get; set; }
            public int AnnualIncome { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, " 
                    + $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        }

        public class PersonBuilder // facade
        {
            // reference
            protected Person person = new Person();
            public PersonJobBuilder Works => new PersonJobBuilder(person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

            public static implicit operator Person(PersonBuilder pb)
            {
                return pb.person;
            }

            public Person Build()
            {
                return person;
            }
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                this.person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }
            public PersonJobBuilder Earning(int annualIncome)
            {
                person.AnnualIncome = annualIncome;
                return this;
            }
        }
    
        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                this.person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }
            
            public PersonAddressBuilder WithPostCode(int postCode)
            {
                person.PostCode = postCode.ToString();
                return this;
            }

            public PersonAddressBuilder InCity(string city)
            {
                person.City = city;
                return this;
            }
        }
    }
}
