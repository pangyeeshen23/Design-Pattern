using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.State
{

    public class SwitchBaseStateMachine
    {
        public enum State
        {
            Locked,
            Failed,
            Unlocked
        }

        public static void Run()
        {
            string code = "1234";
            State state = State.Locked;
            StringBuilder entry = new StringBuilder();
            while(true)
            {
                switch(state)
                {
                    case State.Locked:
                        entry.Append(Console.ReadKey().KeyChar);
                        if (entry.ToString() == code)
                        {
                            state = State.Unlocked;
                            break;
                        }
                        if (!code.StartsWith(entry.ToString()))
                        {
                            goto case State.Failed;
                        }
                        break;
                    case State.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("FAILED");
                        entry.Clear();
                        state = State.Locked;
                        break;
                    case State.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("UNLOCKED");
                        break;
                }
            }
        }
    }
}
