using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;

namespace DesignPattern.Proxy
{
    public class DynamicProxy
    {

        public interface IBankAccount
        {
            void Deposit(int amount);
            bool Withdraw(int amount);
            string ToString();
        }

        public class BankAccount : IBankAccount
        {
            private int balance;
            private int overdraftLimit = -500;

            public void Deposit(int amount)
            {
                balance += amount;
                Console.WriteLine($"Deposited ${amount}, balance is now ${balance}");
            }

            public bool Withdraw(int amount)
            {
                if (balance - amount >= overdraftLimit)
                {
                    balance -= amount;
                    Console.WriteLine($"Withdraw ${amount}, balance is now ${balance}");
                    return true;
                }
                return false;
            }

            public override string ToString()
            {
                return $"{nameof(balance)}: {balance}";
            }
        }

        public void Run()
        {
            BankAccount ba = new BankAccount();
            ba.Deposit(100);
            ba.Withdraw(50);
            Console.WriteLine(ba);

            var ba2 = Log<BankAccount>.As<IBankAccount>();
            ba2.Deposit(100);
            ba2.Withdraw(50);
            Console.WriteLine(ba2);
        }

        public class Log<T> : DynamicObject
            where T : class, new ()
        {
            private Dictionary<string, int> methodCallCount = new Dictionary<string, int>();

            private readonly T subject = new T();

            public Log(T subject)
            {
                this.subject = subject;
            }
            
            public static I As<I>() where I : class
            {
                if(typeof(I).IsInterface == false)
                    throw new ArgumentException("I must be an interface type");

                return new Log<T>(new T()).ActLike<I>();
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
            {
                try
                {
                    Console.WriteLine($"Invoking {subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",", args)}]");

                    if(methodCallCount.ContainsKey(binder.Name)) methodCallCount[binder.Name]++;
                    else methodCallCount.Add(binder.Name, 1);

                    result = subject.GetType().GetMethod(binder.Name).Invoke(subject, args);
                    return true;
                }
                catch(Exception ex)
                {
                    result = null;
                    return false;
                }
            }

            public string Info
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var kv in methodCallCount)
                        sb.AppendLine($"{kv.Key} called {kv.Value} time(s)");
                    return sb.ToString();
                }
            }

            public override string ToString()
            {
                return $"{Info}\n{subject}";
            }
        }


    }
}
