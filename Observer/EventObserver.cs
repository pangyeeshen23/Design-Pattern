using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Observer
{
    public class EventObserver
    {

        public class FallIllsEventArgs
        {
            public string Address;
        }

        public class Person
        {
            public void CatchACold()
            {
                FallsIll?.Invoke(this, new FallIllsEventArgs { Address = "123 London Road"});
            }

            public event EventHandler<FallIllsEventArgs> FallsIll;
        }

        public class Demo
        {
            public void Run()
            {
                Person person = new Person();
                person.FallsIll += CallDoctor;
                person.CatchACold();
                person.FallsIll -= CallDoctor;
            }

            private static void CallDoctor(object? sender, FallIllsEventArgs e)
            {
                Console.WriteLine($"A doctor has been called to {e.Address}");
            }
        }
    }
}
