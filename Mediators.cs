using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Mediator;

namespace DesignPattern
{
    public class Mediators
    {
        public void Run()
        {
            //BasicMediator basicMediator = new BasicMediator();
            //basicMediator.Run();

            EventBroker eventBroker = new EventBroker();
            eventBroker.Run();
        }
    }
}
