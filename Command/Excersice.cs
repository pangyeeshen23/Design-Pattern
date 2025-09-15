using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Command.Excersice;

namespace DesignPattern.Command
{
    class Excersice
    {
        public class Command : ICommand
        {
            public enum Action
            {
                Deposit,
                Withdraw
            }

            public Action TheAction;
            public int Amount;
            public bool Success;
            public Account Account;

            public void Call()
            {
                switch (TheAction)
                {
                    case Command.Action.Deposit:
                        Success = this.Account.Deposit(Amount);
                        break;
                    case Command.Action.Withdraw:
                        Success = this.Account.Withdraw(Amount);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public interface ICommand
        {
            void Call();
        }

        public class Account
        {
            public int Balance { get; set; }

            public void Process(Command c)
            {
                c.Account = this;
                c.Call();
            }

            public bool Deposit(int amount)
            {
                this.Balance += amount;
                return true;
            }

            public bool Withdraw(int amount)
            {
                if (this.Balance >= amount)
                {
                    this.Balance -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
