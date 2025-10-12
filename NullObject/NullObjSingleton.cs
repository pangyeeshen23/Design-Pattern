using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.NullObject
{
    public class NullObjSingleton
    {
        public interface ILog
        {
            void Warn();

            public static ILog Null => NullLog.Instance;

            private sealed class NullLog : ILog
            {
                public NullLog()
                {

                }

                private static Lazy<NullLog> instance = new Lazy<NullLog>(() => new NullLog());

                public static ILog Instance => instance.Value;

                public void Warn()
                {

                }
            }

        }

        public class Program
        {
            public static void Run()
            {
                ILog.Null.Warn();
            }
        }
    }
}
