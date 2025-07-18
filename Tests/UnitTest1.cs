using System.ComponentModel;
using Autofac;
using DesignPattern;
using DesignPattern.Singleton;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Singleton_Instance_Should_Be_One_Test()
        {
            SingletonDatabase db = SingletonDatabase.Instance;
            SingletonDatabase db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void Singleton_Total_Population_Test()
        {
            SingletonsRecordFinder finder = new SingletonsRecordFinder();
            var names = new List<string> { "Seoul", "Mexico City" };
            int totalPopulation = finder.GetTotalPopulation(names);
            Assert.That(totalPopulation, Is.EqualTo(17400000 + 17500000));
        }

        [Test]
        public void Dummy_Total_Population_Test()
        {
            IDatabase database = DummyDatabase.Instance;
            ConfigurableRecordFinder finder = new ConfigurableRecordFinder(database);
            var names = new List<string> { "Alpha", "Beta" };
            int totalPopulation = finder.GetTotalPopulation(names);
            Assert.That(totalPopulation, Is.EqualTo(1 + 2));
        }

        [Test]
        public void DIP_Population_Test()
        {
            ContainerBuilder builder = new ContainerBuilder();
            // this will tell the dependency injection container to only have one instance of the OrdinaryDatabase
            builder.RegisterType<OrdinaryDatabase>().As<IDatabase>().SingleInstance();
            builder.RegisterType<ConfigurableRecordFinder>();
            using (var container = builder.Build())
            {
                var rf = container.Resolve<ConfigurableRecordFinder>();
                int totalPopulation = rf.GetTotalPopulation(new List<string> { "Seoul", "Mexico City" });
                Assert.That(totalPopulation, Is.EqualTo(17400000 + 17500000));
            }
        }

    }
}