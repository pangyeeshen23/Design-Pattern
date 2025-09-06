using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Proxy
{
    class Excersize
    {
        public class Person
        {
            public int Age { get; set; }

            public string Drink()
            {
                return "drinking";
            }

            public string Drive()
            {
                return "driving";
            }

            public string DrinkAndDrive()
            {
                return "driving while drunk";
            }
        }

        public class ResponsiblePerson
        {
            private readonly Person person;
            public ResponsiblePerson(Person person)
            {
                this.person = person;
            }

            public int Age 
            { 
                get 
                {
                    return this.person.Age; 
                } 
                set 
                {
                    this.person.Age = value;
                }  
            }
            public string Drink()
            {
                if (this.person.Age < 18) return "too young";
                return this.person.Drink();
            }

            public string Drive()
            {
                if(this.person.Age < 16) return "too young";
                return this.person.Drive();
            }

            public string DrinkAndDrive()
            {
                return "dead";
            }
        }
    }
}
