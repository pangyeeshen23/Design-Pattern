using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.NullObject
{
    public interface ILog
    {
        int RecordLimit { get; }
        int RecordCount { get; set; }
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    public class NullLog : ILog
    {
        // todo
        public int RecordLimit { get; } = int.MaxValue;

        public int RecordCount { get; set; } = 0;

        public void LogInfo(string message)
        {
            RecordCount++;
        }
    }

    public class Excersice
    {
        public static void Run()
        {
            var account = new Account(new NullLog());
            account.SomeOperation();
        }
    }
}
