using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Singleton
{
    // Per thread singleton means that every thread will have it own instance of the object
    // so it does not work like the usual singleton that limit it to the entire application
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> instance =
            new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());

        public int Id;

        public PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => instance.Value;
    }
}
