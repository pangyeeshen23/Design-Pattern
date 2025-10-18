using DesignPattern.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPattern.Observer.EventObserver;

namespace DesignPattern
{
    public class Observers
    {
        public void Run()
        {
            //EventObserver.Demo eventObserver = new EventObserver.Demo();
            //eventObserver.Run();

            //WeakEventPattern weakEvent = new WeakEventPattern();
            //weakEvent.Run();

            //SpecialInterface.Program.Run();

            //MarketObserver.Program.Run();

            //BidirectionalObserver.Run();

            //PropertyDependencies.Run();

            EventSubscriptions.Program.Run();
        }
    }
}
