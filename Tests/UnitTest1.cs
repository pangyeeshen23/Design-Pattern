using DesignPattern.Singleton;

namespace Tests
{
    public class Tests
    {
        [TestFixture]
        public class SingletonTest_Should_Contain_One_Instance
        {
            [Test]
            public void IsSingletonTest()
            {
                Lazy<SingletonDatabase> db = SingletonDatabase.Instance;
                Lazy<SingletonDatabase> db2 = SingletonDatabase.Instance;
                Assert.That(db, Is.SameAs(db2));
                Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
            }
        }
    }
}