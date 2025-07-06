using System;
using System.Collections.Generic;

namespace Coding.Exercise
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class Factory
        {
            private List<Person> people = new List<Person>();

            public Person CreatePerson(string name)
            {
                int id = people.Count;
                Person person = new Person()
                {
                    Id = id,
                    Name = name
                };
                people.Add(person);
                return person;
            }
        }

        public override string ToString()
        {
            return $"Id : {Id}, Name : {Name} ";
        }
    }
}
