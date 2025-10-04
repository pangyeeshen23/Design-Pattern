namespace DesignPattern.Memento
{
    public class BasicMemento
    {
        public class BankAccount
        {
            private int _balance;

            public BankAccount(int balance)
            {
                this._balance = balance;
            }
            public Memento Deposit(int amount)
            {
                _balance += amount;
                return new Memento(_balance);
            }

            public void Restore(Memento m)
            {
                _balance = m.Balance;
            }

            public override string ToString()
            {
                return $"Balance: {_balance}";
            }
        }

        public class Memento
        {
            public int Balance { get; }
            public Memento(int balance)
            {
                this.Balance = balance;
            }

           
        }

        public void Run()
        {
            BankAccount ba = new BankAccount(100);
            Memento m1 = ba.Deposit(50); // 150
            Memento m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            ba.Restore(m1); // back to 150
            Console.WriteLine(ba);

            ba.Restore(m2); // back to 175
            Console.WriteLine(ba);
        }
    }
}
