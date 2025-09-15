using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Command
{
    public class CompositeCommand
    {
        public class BankAccount
        {
            private int balance;
            private int overdraftLimit = -500;
            public bool Success { get; set; }

            public bool Deposit(int amount)
            {
                balance += amount;
                Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
                return true;
            }

            public bool Withdraw(int amount)
            {
                if (balance - amount >= overdraftLimit)
                {
                    balance -= amount;
                    Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
                    return true;
                }
                return false;
            }

            public override string ToString()
            {
                return $"Balance: {balance}";
            }
        }

        public interface ICommand
        {
            void Call();
            void Undo();
            bool Success { get; set; }
        }

        public class BankAccountCommand : ICommand
        {
            private BankAccount account = new BankAccount();
            private Action action;
            private int amount;
            public bool Success { get;  set; }

            public BankAccountCommand(BankAccount account, Action action, int amount)
            {
                if (account == null) throw new ArgumentNullException(paramName: nameof(account));
                this.account = account;
                this.action = action;
                this.amount = amount;
            }

            public enum Action
            {
                Deposit,
                Withdraw
            }

            public void Call()
            {
                switch (action)
                {
                    case Action.Deposit:
                        Success = account.Deposit(amount);
                        break;
                    case Action.Withdraw:
                        Success = account.Withdraw(amount);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public void Undo()
            {
                if (!Success) return;
                switch (action)
                {
                    case Action.Deposit:
                        account.Withdraw(amount);
                        break;
                    case Action.Withdraw:
                        account.Deposit(amount);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
        {
            public CompositeBankAccountCommand()
            {
                
            }

            public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
            {
                
            }

            public virtual bool Success { 
                get
                {
                    return this.All(c => c.Success);
                }
                set
                {
                    foreach (var cmd in this)
                    {
                        cmd.Success = value;
                    }
                }
            }

            public virtual void Call()
            {
                // A -> B
                // A 100
                // 1000

                ForEach(c => c.Call());
            }

            public virtual void Undo()
            {
                IEnumerable<BankAccountCommand> reversed = ((IEnumerable<BankAccountCommand>)this).Reverse();
                foreach(var cmd in reversed)
                {
                    cmd.Undo();
                }
            }
        }

        public class MoneyTransferCommand : CompositeBankAccountCommand
        {
            public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
            {
                AddRange(new[]
                {
                    new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
                    new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
                });
            }

            public override void Call()
            {
                BankAccountCommand? last = null;
                foreach(var cmd in this)
                {
                    if (last == null || last.Success)
                    {
                        cmd.Call();
                        last = cmd;
                    }
                    else
                    {
                        cmd.Success = false;
                    }
                }
            }
        }


        public void Run()
        {
            //BankAccount ba = new BankAccount();
            //BankAccountCommand deposit = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
            //BankAccountCommand withdraw = new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50);
            //CompositeBankAccountCommand commands = new CompositeBankAccountCommand(new[] { deposit, withdraw });
            //commands.Call();
            //Console.WriteLine(ba);
            //commands.Undo();
            //Console.WriteLine(ba);

            BankAccount from = new BankAccount();
            BankAccount to = new BankAccount();
            from.Deposit(100);
            MoneyTransferCommand mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();
            Console.WriteLine(from);
            Console.WriteLine(to);
            mtc.Undo();
            Console.WriteLine(from);
            Console.WriteLine(to);
        }
    }
}
