using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.State
{
    public static class SwitchExpression
    {
        public enum Chest
        {
            Open,
            Closed,
            Locked
        }

        public enum Action
        {
            Open,
            Close
        }

        public static Chest Manipulate(Chest chest, Action action, bool haveKey) {
            switch(chest, action, haveKey)
            {
                case (Chest.Locked, Action.Open, true):
                    chest = Chest.Open;
                    break;
                case (Chest.Closed, Action.Open, false):
                    chest = Chest.Open;
                    break;
                case (Chest.Open, Action.Open, true):
                    chest = Chest.Locked;
                    break;
                case (Chest.Open, Action.Close, false):
                    chest = Chest.Closed;
                    break;
                default:
                    break;
            }
            return chest;
        }

        public static void Run()
        {
            var chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");
        }
    }
}
