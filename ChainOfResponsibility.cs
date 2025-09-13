using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.ChainOfResponsibilitys;

namespace DesignPattern
{
    class ChainOfResponsibility
    {
        public void Run()
        {
            //MethodChain methodChain = new MethodChain();
            //methodChain.Run();

            //BrokerChain brokerChain = new BrokerChain();
            //brokerChain.Run();

            Exercise exercise = new Exercise();
            exercise.Run();
        }
    }
}
