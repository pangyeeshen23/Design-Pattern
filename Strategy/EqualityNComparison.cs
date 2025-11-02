using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Strategy
{
    public class EqualityNComparison
    {
        class Person : IComparable<Person>, IComparable
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(int id, string name, int age)
            {
                Id = id;
                Name = name;
                Age = age;
            }

            public int CompareTo(Person? other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(this, other)) return 1;
                return Id.CompareTo(other.Id);
            }

            public int CompareTo(object? obj)
            {
                if (ReferenceEquals(null, obj)) return 1;
                if (ReferenceEquals(this, obj)) return 0;
                return obj is Person other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Person)}");
            }

            // Remove 'public' from the explicit interface implementation
            public sealed class NameRelationComparer : IComparer<Person>
            {
                int IComparer<Person>.Compare(Person? x, Person? y)
                {
                    if (ReferenceEquals(x, y)) return 0;
                    if (ReferenceEquals(null, y)) return 1;
                    if (ReferenceEquals(null, x)) return -1;
                    return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
                }
            }

            public static bool operator < (Person left, Person right)
            {
                return Comparer<Person>.Default.Compare(left, right) < 0;
            }

            public static bool operator > (Person left, Person right)
            {
                return Comparer<Person>.Default.Compare(left, right) > 0;
            }

            public static bool operator <=(Person left, Person right)
            {
                return Comparer<Person>.Default.Compare(left, right) <= 0;
            }

            public static bool operator >= (Person left, Person right)
            {
                return Comparer<Person>.Default.Compare(left, right) >= 0;
            }

            public static IComparer<Person> NameComparer { get; } = new NameRelationComparer();
        }

        public class Comparisor
        {
            public static void Run()
            {
                List<Person> people = new List<Person>()
                {
                    new Person(0, "Ethan", 23),
                    new Person(1, "Elliot", 43),
                    new Person(2, "Alice", 44),
                    new Person(3, "Berlin", 25)
                };
                Console.WriteLine("Sort By Id :");
                people.Sort(); // default sort - uses id by default because we declare the person as IComparable and it has Compare To etc.
                Print(people);

                Console.WriteLine("Sort By Name :");
                //people.Sort((x,y) => x.Name.CompareTo(y.Name)); // name sort - uses name to sort the person.
                people.Sort(Person.NameComparer);
                Print(people);
            }

            private static void Print(List<Person> persons)
            {
                foreach(Person person in persons)
                {
                    Console.WriteLine($"Id : {person.Id}, Name : {person.Name}, Age : {person.Age}");
                }
            }
        }

    }
}
