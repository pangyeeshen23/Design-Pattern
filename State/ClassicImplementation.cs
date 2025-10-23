using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.State
{
    public class ClassicImplementation
    {
        public class Switch
        {
            public State State = new OffState();

            public void On()
            {
                State.On(this);
            }

            public void Off()
            {
                State.Off(this);
            }
        }

        public abstract class State
        {
            public virtual void On(Switch sw)
            {
                Console.WriteLine("Light is already on.");
            }

            public virtual void Off(Switch sw)
            {
                Console.WriteLine("Light is already off.");
            }
        }

        public class OffState : State
        {
            public OffState()
            {
                Console.WriteLine("Light Turned Off.");

            }

            public override void On(Switch sw)
            {
                Console.WriteLine("Turning Light On");
                sw.State = new OnState();
            }
        }

        public class OnState : State
        {
            public OnState()
            {
                Console.WriteLine("Light Turned On.");
            }

            public override void Off(Switch sw)
            {
                Console.WriteLine("Turning Light Off");
                sw.State = new OffState();
            }

        }

        public void Run()
        {
            Switch sw = new Switch();
            sw.On();
            sw.Off();
            sw.Off();
        }
    }
}
