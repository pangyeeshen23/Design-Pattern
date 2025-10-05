namespace DesignPattern.Memento
{
    public class BasicMemento
    {
        public class BankAccount
        {
            private int _balance;
            private List<Memento> _changes = new List<Memento>();
            private int current;

            public BankAccount(int balance)
            {
                this._balance = balance;
                _changes.Add(new Memento(balance));
            }

            public Memento Deposit(int amount)
            {
                _balance += amount;
                var m = new Memento(_balance);
                _changes.Add(m);
                current++;
                return m;
            }

            public Memento? Restore(Memento m)
            {
                if(m != null)
                {
                    _balance = m.Balance;
                    _changes.Add(m);
                    current++;
                    return m;
                }
                return null;
            }

            public Memento? Undo()
            {
                if (current > 0)
                {
                    var m = _changes[--current];
                    _balance = m.Balance;
                    return m;
                }
                return null;
            }

            public Memento? Redo()
            {
                if(current + 1 < _changes.Count)
                {
                    Memento m = _changes[++current];
                    _balance = m.Balance;
                    return m;
                }
                return null;
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

            ba.Undo();
            Console.WriteLine($"Undo 1: {ba}");

            ba.Undo();
            Console.WriteLine($"Undo 2: {ba}");

            ba.Redo();
            Console.WriteLine($"Redo 1: {ba}");

            ba.Redo();
            Console.WriteLine($"Redo 2: {ba}");

            ba.Restore(m1); // back to 150
            Console.WriteLine(ba);

            ba.Restore(m2); // back to 175
            Console.WriteLine(ba);
        }
    }
}
