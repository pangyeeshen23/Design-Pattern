using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.State
{
    public static class Excersice
    {
        public class CombinationLock
        {
            public enum LockState
            {
                Locked,
                Open,
                Error
            }

            private readonly int[] _combination;
            private string _status;

            public CombinationLock(int[] combination)
            {
                _combination = combination;
                State = LockState.Locked;
                
            }

            public LockState State;

            // you need to be changing this on user input
            public string Status => GetStatus();

            public string GetStatus()
            {
                switch(State)
                {
                    case LockState.Locked:
                        if(_status == "LOCKED" || string.IsNullOrEmpty(_status)) _status = "LOCKED";
                        break;
                    case LockState.Open:
                        _status = "OPEN";
                        break;
                    case LockState.Error:
                        _status = "ERROR";
                        break;
                }
                return _status;
            }

            public void EnterDigit(int digit)
            {
                if (State != LockState.Locked) return;
                if (Status == "LOCKED") _status = "";
                _status += digit.ToString();
                if (Status.Length == _combination.Length)
                {
                    bool isValid = true;
                    for(int i = 0; i < Status.Length; i++)
                    {
                        if (Status[i].ToString() != _combination[i].ToString()) isValid = false; 
                    }
                    if(isValid)
                    {
                        State = LockState.Open;
                    }
                    else
                    {
                        State = LockState.Error;
                    }
                }
            }
        }

        public static void Run()
        {
            CombinationLock locker = new CombinationLock(new int[] { 1, 2, 3, 4 });
            while(true)
            {
                Console.WriteLine($"Lock status: {locker.Status}");
                var key = Console.ReadKey();
                if (char.IsDigit(key.KeyChar))
                {
                    locker.EnterDigit(int.Parse(key.KeyChar.ToString()));
                }
            }
        }
    }
}
