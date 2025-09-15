using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Command
{
   

    class BasicCommand
    {
        public class BankAccount
        {
            private int balance;
            private int overdraftLimit = -500;

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
        }

        public class BankAccountCommand : ICommand
        {
            private BankAccount account = new BankAccount();
            private Action action;
            private int amount;
            private bool succeeded;

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
                        this.succeeded = account.Deposit(amount);
                        break;
                    case Action.Withdraw:
                        this.succeeded = account.Withdraw(amount);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public void Undo()
            {
                if (!this.succeeded) return;
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

        public void Run()
        {
            BankAccount ba = new BankAccount();
            List<BankAccountCommand> command = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000)
            };
            Console.WriteLine(ba);
            foreach (var cmd in command)
                cmd.Call();
            Console.WriteLine(ba);
            command.Reverse();
            foreach (var cmd in command)
            {
                cmd.Undo();
            }
            Console.WriteLine(ba);
        }
    }
}
