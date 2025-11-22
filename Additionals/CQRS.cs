
namespace DesignPattern.Additionals
{
    // CQRS : Command Query Responsibility Segregation
    // CQS  : Command Query Separation


    public class CQRS
    {
        public class Person
        {
            private int age;
            private EventBroker _broker;
            private string name;

            public Person(EventBroker broker)
            {
                _broker = broker;
                broker.Commands += BrokerOnCommands;
                broker.Queries += BrokerOnQueries;
            }

            private void BrokerOnCommands(object? sender, Command e)
            {
                ChangeAgeCommand cmd = (ChangeAgeCommand)e;
                if(cmd != null && cmd.Target == this)
                {
                    if(cmd.Register) _broker.AllEvents.Add(new AgeChangeEvent(this, age, cmd.Age));
                    age = cmd.Age;
                }
            }

            public void BrokerOnQueries(object? sender, Query e)
            {
                var ac = e as AgeQuery;
                if(ac != null && ac.Target == this)
                {
                    ac.Result = age;
                }
            }
        }

        public class EventBroker
        {
            // 1. Define events that happened.
            public IList<Event> AllEvents = new List<Event>();
            // 2. Commands
            public event EventHandler<Command> Commands;
            // 3. Query
            public event EventHandler<Query> Queries;

            public void Command(Command c)
            {
                Commands?.Invoke(this, c);
            }

            public T Query<T>(Query q)
            {
                Queries?.Invoke(this, q);
                return (T)q.Result;
            }

            public void UndoLast()
            {
                var e = AllEvents.LastOrDefault();
                var ac = e as AgeChangeEvent;
                if (ac != null)
                {
                    Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Register = false});
                    AllEvents.Remove(e);
                }
            }
        }

        public class Query
        {
            public object Result;
        }

        public class AgeQuery : Query
        {
            public Person Target;
        }

        public class Command : EventArgs
        {
            public bool Register = true;
        }

        public class ChangeAgeCommand : Command
        {
            public Person Target;
            public int Age;

            public ChangeAgeCommand(Person target, int age)
            {
                Target = target;
                Age = age;
            }
        }


        public class Event
        {
            
        }

        public class AgeChangeEvent : Event
        {
            public Person Target;
            public int OldValue, NewValue;

            public AgeChangeEvent(Person target, int oldVal, int newVal)
            {
                Target = target;
                OldValue = oldVal;
                NewValue = newVal;
            }

            public override string ToString()
            {
                return $"Age changed from {OldValue} to {NewValue}";
            }
        }

        public static void Execute()
        {
            EventBroker broker = new EventBroker();
            Person p = new Person(broker);
            broker.Command(new ChangeAgeCommand(p, 123));

            foreach(var e in broker.AllEvents)
            {
                Console.WriteLine(e);
            }
            int age;
            age = broker.Query<int>(new AgeQuery { Target = p });
            Console.WriteLine(age);
            broker.UndoLast();
            foreach (var e in broker.AllEvents)
            {
                Console.WriteLine(e);
            }

            age = broker.Query<int>(new AgeQuery { Target = p });
            Console.WriteLine(age);

            Console.ReadKey();
        }

    }
}
