

using DesignPattern.Proxy;

namespace Tests
{
    [TestFixture]
    class ProxyTest
    {

        [Test]
        public void Test_Composite_Proxy()
        {
            FlyweightTest test = new FlyweightTest();

            CompositeProxy proxy = new CompositeProxy();
            proxy.Run();

            test.ForceGC();
            FlyweightTest.MemoryChecker.Check(memory =>
            {
                Console.WriteLine($"Managed Heap: {memory.ManagedBytes} bytes");
                Console.WriteLine($"Private Memory: {memory.PrivateBytes} bytes");
                Console.WriteLine($"Working Set: {memory.WorkingSet} bytes");
            });
        }

        [Test]
        public void Test_Composite_Optimized_Proxy()
        {
            FlyweightTest test = new FlyweightTest();

            CompositeProxy proxy = new CompositeProxy();
            proxy.RunOptimized();

            test.ForceGC();
            FlyweightTest.MemoryChecker.Check(memory =>
            {
                Console.WriteLine($"Managed Heap: {memory.ManagedBytes} bytes");
                Console.WriteLine($"Private Memory: {memory.PrivateBytes} bytes");
                Console.WriteLine($"Working Set: {memory.WorkingSet} bytes");
            });
        }
    }
}
