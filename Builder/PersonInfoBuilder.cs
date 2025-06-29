using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    // Fluent Builder
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public static Builder New => new Builder();

        public class Builder : PersonSalaryBuilder<Builder>
        {

        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        /// Self - PersonInfoBuilder<PersonJobBuilder<PersonSalaryBuilder<Builder>>>
        public class PersonInfoBuilder<Self> : PersonBuilder where Self : PersonInfoBuilder<Self>
        {
            public Self SetName(string name)
            {
                person.Name = name;
                Console.WriteLine(typeof(Self));
                return (Self)this;
            }
        }

        // This class is a type of PersonInfoBuilder<PersonJobBuilder<PersonSalaryBuilder<Builder>>>
        // because of the inheritance chain.
        public class PersonJobBuilder<Self> : PersonInfoBuilder<PersonJobBuilder<Self>> where Self : PersonJobBuilder<Self>
        {
            public PersonJobBuilder()
            {

            }

            public Self WorkAsA(string position)
            {
                person.Position = position;
                return (Self)this;
            }
        }

        public class PersonSalaryBuilder<Self> : PersonJobBuilder<PersonSalaryBuilder<Self>> where Self : PersonSalaryBuilder<Self>
        {
            public PersonSalaryBuilder()
            {

            }

            public Self Earn(int salary)
            {
                person.Salary = salary;
                return (Self)this;
            }
        }
    }

   
}
