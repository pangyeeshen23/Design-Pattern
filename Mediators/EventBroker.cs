using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Linq;
using Autofac;

namespace DesignPattern.Mediators
{
    public class EventBroker
    {
        public class EventBrokerImp : IObservable<PlayerEvent>
        {
            Subject<PlayerEvent> subscribtions = new Subject<PlayerEvent>();

            public IDisposable Subscribe(IObserver<PlayerEvent> observer)
            {
                return subscribtions.Subscribe(observer);
            }

            public void Publish(PlayerEvent pe)
            {
                subscribtions.OnNext(pe);
            }
        }

        public class Actor
        {
            protected EventBrokerImp _broker;
            public Actor(EventBrokerImp broker)
            {
                _broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
            }
        }

        public class FootballPlayer : Actor
        {
            public string Name { get; set; }
            public int GoalsScored { get; set; }

            public void Score()
            {
                GoalsScored++;
                _broker.Publish(new PlayerScoredEvent { Name = Name, GoalsScored = GoalsScored });
            }

            public void AssaultRef()
            {
                _broker.Publish(new PlayerSentOffEvent { Name = Name, Reason = "violence" });
            }

            public FootballPlayer(EventBrokerImp broker, string name) : base(broker)
            {
                if(name == null) throw new ArgumentNullException(paramName: nameof(name));
                Name = name;
                broker.OfType<PlayerScoredEvent>()
                    .Where(ps => !ps.Name.Equals(name))
                    .Subscribe(ps =>
                    {
                        Console.WriteLine($"{name}: Nicely done, {ps.Name}! It's your {ps.GoalsScored} goals.");
                    });
                broker.OfType<PlayerSentOffEvent>()
                    .Where(ps => !ps.Name.Equals(name))
                    .Subscribe(ps => Console.WriteLine($"{name}: see you in the lockers, {ps.Name}"));
            }
        }

        public class FootballCoach : Actor
        {
            public FootballCoach(EventBrokerImp broker) : base(broker)
            {
                broker.OfType<PlayerScoredEvent>().Subscribe(pe =>
                {
                    if (pe.GoalsScored < 3)
                        Console.WriteLine($"Coach: well done, {pe.Name} !");
                });

                broker.OfType<PlayerSentOffEvent>().Subscribe(pe =>
                {
                    if (pe.Reason == "violence")
                        Console.WriteLine($"Coach: how could you, {pe.Name}.");
                });
            }
        }

        public class PlayerEvent
        {
            public string? Name { get; set; }
        }

        public class PlayerScoredEvent : PlayerEvent
        {
            public int GoalsScored { get; set; }
        }

        public class PlayerSentOffEvent : PlayerEvent
        {
            public string? Reason { get; set; }
        }

        public void Run()
        {
            ContainerBuilder container = new ContainerBuilder();
            container.RegisterType<EventBrokerImp>().SingleInstance();
            container.RegisterType<FootballCoach>();
            container.Register((c, p) => new FootballPlayer(c.Resolve<EventBrokerImp>(), p.Named<string>("name")));

            using (var c = container.Build())
            {
                FootballCoach coach = c.Resolve<FootballCoach>();
                FootballPlayer player1 = c.Resolve<FootballPlayer>(new NamedParameter("name","John"));
                FootballPlayer player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Score();
                player1.Score();
                player1.Score();
                player1.AssaultRef();
                player2.Score();
            }
        }
    }
}
