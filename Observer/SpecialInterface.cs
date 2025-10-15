using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Observer
{
    public class SpecialInterface
    {
        public class Event
        { 

        }

        public class FallIllsEvent : Event
        {
            public string Address;
            
        }

        public class Person : IObservable<Event>
        {
            private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();

            public IDisposable Subscribe(IObserver<Event> observer)
            {
                var subscription = new Subscription(this, observer);
                subscriptions.Add(subscription);
                return subscription;
            }

            public void FallsIll()
            {
                foreach(Subscription sub in subscriptions)
                {
                    sub.Observer.OnNext(
                        new FallIllsEvent() { Address = "123 London Rd"}
                    );
                }
            }

            private class Subscription : IDisposable
            {
                private readonly Person _person;
                public readonly IObserver<Event> Observer;

                public Subscription(Person person, IObserver<Event> observer)
                {
                    _person = person;
                    Observer = observer;
                }

                public void Dispose()
                {
                    _person.subscriptions.Remove(this);
                }
            }
        }

        public class Program : IObserver<Event>
        {
            public Program()
            {
                Person person = new Person();
                using IDisposable sub = person.Subscribe(this);

                person.FallsIll();
            }

            public static void Run()
            {
                new Program();

            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(Event value)
            {
                if(value is FallIllsEvent args)
                    Console.WriteLine($"A doctor is required at {args.Address}");
            }

        }

    }
}
