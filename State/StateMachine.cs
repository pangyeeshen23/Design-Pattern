using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.State.ClassicImplementation;

namespace DesignPattern.State
{

    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HungUp,
        CallConnected,
        PlaceOnHold,
        TakenOffHold,
        LeftMessage
    }

    public class StateMachine
    {
        private static Dictionary<State, List<(Trigger, State)>> rules = new Dictionary<State, List<(Trigger, State)>>
        {
            [State.OffHook] = new List<(Trigger, State)>
            {
                (Trigger.CallDialed, State.Connecting)
            },
            [State.Connecting] = new List<(Trigger, State)>
            {
                (Trigger.HungUp, State.OffHook),
                (Trigger.CallConnected, State.Connected),
            },
            [State.Connected] = new List<(Trigger, State)>
            {
                (Trigger.LeftMessage, State.OffHook),
                (Trigger.HungUp, State.OffHook),
                (Trigger.PlaceOnHold, State.OnHold),
            },
            [State.OnHold] = new List<(Trigger, State)>
            {
                (Trigger.TakenOffHold, State.Connected),
                (Trigger.HungUp, State.OffHook)
            }
        };

        public static void Run()
        {
            State initialState = State.OffHook;
            while(true)
            {
                Console.WriteLine($"The phone is currently {initialState}");
                Console.WriteLine("Select a trigger:");
                for (var i = 0; i < rules[initialState].Count; i++)
                {
                    (Trigger trigger, State state) = rules[initialState][i];
                    Console.WriteLine($"{i}. {trigger}");
                }
                int input = int.Parse(Console.ReadLine() ?? "0");
                (Trigger _, State s) = rules[initialState][input];
                initialState = s;
            }
        }
    }
}
