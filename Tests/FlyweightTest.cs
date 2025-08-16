using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Flyweight;
using JetBrains.dotMemoryUnit;
using JetBrains.dotMemoryUnit.Kernel;

namespace Tests
{
    [TestFixture]
    public class FlyweightTest
    {
        [Test]
        public void Test_User()
        {
            var firstNames = Enumerable.Range(0, 100)
                .Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100)
                .Select(_ => RandomString());

            var users = new List<DefaultFW.User>();
            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new DefaultFW.User($"{firstName} {lastName}"));
                }
            }

            ForceGC();
            MemoryChecker.Check(memory =>
            {
                Console.WriteLine($"Managed Heap: {memory.ManagedBytes} bytes");
                Console.WriteLine($"Private Memory: {memory.PrivateBytes} bytes");
                Console.WriteLine($"Working Set: {memory.WorkingSet} bytes");
            });
        }

        [Test]
        public void Test_Optimized_User()
        {
            var firstNames = Enumerable.Range(0, 100)
                .Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100)
                .Select(_ => RandomString());

            var users = new List<DefaultFW.OptimizedUser>();
            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    users.Add(new DefaultFW.OptimizedUser($"{firstName} {lastName}"));
                }
            }

            ForceGC();
            MemoryChecker.Check(memory =>
            {
                Console.WriteLine($"Managed Heap: {memory.ManagedBytes} bytes");
                Console.WriteLine($"Private Memory: {memory.PrivateBytes} bytes");
                Console.WriteLine($"Working Set: {memory.WorkingSet} bytes");
            });
        }

        private class MemorySnapshot
        {
            public long ManagedBytes { get; }
            public long PrivateBytes { get; }
            public long WorkingSet { get; }

            public MemorySnapshot()
            {
                ManagedBytes = GC.GetTotalMemory(forceFullCollection: true);

                using var proc = Process.GetCurrentProcess();
                PrivateBytes = proc.PrivateMemorySize64;
                WorkingSet = proc.WorkingSet64;
            }
        }


        private static class MemoryChecker
        {
            public static void Check(Action<MemorySnapshot> action)
            {
                MemorySnapshot snapShot = new MemorySnapshot();
                action(snapShot);
            }
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10).Select(i =>(char)('a' + rand.Next(26))).ToArray()
            );
        }
    }
}
