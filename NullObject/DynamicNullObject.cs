using ImpromptuInterface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.NullObject
{
    public class DynamicNullObject
    {
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

        public interface ILog
        {
            void Info(string msg);
            void Warn(string msg);
        }

        public class Null<TInterface> : DynamicObject where TInterface : class
        {
            public static TInterface Instance => new Null<TInterface>().ActLike<TInterface>();

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = null;
                return true;
            }
        }

        public static class Demo
        {
            public static void Run()
            {
                var log = Null<ILog>.Instance;
                log.Info("foo");
                var ba = new BankAccount(log);
                ba.Deposit(100);
            }
        }
    }

}
