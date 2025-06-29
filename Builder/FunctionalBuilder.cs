
using static DesignPattern.Builder.FunctionalBuilder;

namespace DesignPattern.Builder
{

    public class FunctionalBuilder
    {
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }
            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }

        }

        public abstract class PersonFuncBuilder<TSubject, TSelf> 
            where TSelf : PersonFuncBuilder<TSubject, TSelf>
            where TSubject : new ()
        {
            private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();
            public TSelf Do(Action<Person> action) => AddAction(action);
            public Person Build()
            {
                return actions.Aggregate(new Person(), (p, f) => f(p));
            }
            private TSelf AddAction(Action<Person> action)
            {
                actions.Add(p =>
                {
                    action(p);
                    return p;
                });
                return (TSelf) this;
            }
        }

        public sealed class PersonBuilder : PersonFuncBuilder<Person, PersonBuilder>
        {
            public PersonBuilder Called(string name) => Do(p => p.Name = name);
        }

        //public sealed class PersonFuncBuilder
        //{
        //    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        //    public PersonFuncBuilder Called(string name) => Do(p => p.Name = name);

        //    public PersonFuncBuilder Do(Action<Person> action) => AddAction(action);

        //    public Person Build()
        //    {
        //        return actions.Aggregate(new Person(), (p, f) => f(p));
        //    }

        //    private PersonFuncBuilder AddAction(Action<Person> action)
        //    {
        //        actions.Add(p =>
        //        {
        //            action(p);
        //            return p;
        //        });
        //        return this;
        //    }
        //}

    }
    //public static class PersonBuilderExtensions
    //{
    //    public static PersonFuncBuilder WorkAs(this PersonFuncBuilder builder, string position) =>
    //        builder.Do(p => p.Position = position);
    //}

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkAs(this PersonBuilder builder, string position) =>
            builder.Do(p => p.Position = position);
    }
}
