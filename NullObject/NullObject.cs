using Autofac;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.NullObject
{
    public class NullObjectPattern
    {
        public interface ILog
        {
            void Info(string msg);
            void Warn(string msg);
        }

        class ConsoleLog : ILog
        {
            public void Info(string msg)
            {
                Console.WriteLine(msg);
            }

            public void Warn(string msg)
            {
                Console.WriteLine("Warning !!! " + msg);
            }
        }

        // Null Object Pattern
        public class NullLog : ILog
        {
            public void Info(string msg)
            {
                
            }

            public void Warn(string msg)
            {
               
            }
        }


        public class BankAccount
        {
            private ILog log;
            private int balance;

            public BankAccount(ILog log)
            {
                this.log = log;
            }

            public void Deposit(int amount)
            {
                balance = amount;
                log?.Info($"Deposited {amount}, balance is now {balance}");
            }
        }

        public class Demo
        {
            public static void Run()
            {
                //var log = new ConsoleLog();
                ContainerBuilder cb = new ContainerBuilder();
                cb.RegisterType<BankAccount>();
                cb.RegisterType<NullLog>().As<ILog>();
                using (var c = cb.Build())
                {
                    BankAccount ba = c.Resolve<BankAccount>();
                    ba.Deposit(100);
                }
            }
        }

    }
}
