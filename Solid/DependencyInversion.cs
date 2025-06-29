using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Solid
{
    public static class DependencyInversion
    {

        public static void MainProcess()
        {
            Person parent = new Person { Name = "John"};
            Person child1 = new Person { Name = "Chris"};
            Person child2 = new Person { Name = "Mary"};

            Relastionships relastionships = new Relastionships();
            relastionships.AddParentAndChild(parent, child1);
            relastionships.AddParentAndChild(parent, child2);

            Research research = new Research(relastionships);
        }

        private enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        private class Person
        {
            public string Name { get; set; }
            public DateTime DateOfBirth;
        }

        private interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        private class Relastionships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
                {
                    yield return r.Item3;
                }
            }

            public List<(Person, Relationship, Person)> Relations { get { return this.relations; } }
        }

        private class Research
        {
            public Research(IRelationshipBrowser relationshipBrowser)
            {
                foreach (var p in relationshipBrowser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}.");
                }
            }
        }
    }

    

}
