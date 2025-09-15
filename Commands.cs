using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Command;

namespace DesignPattern
{
    class Commands
    {
        public void Run()
        {
            //BasicCommand command = new BasicCommand();
            //command.Run();

            CompositeCommand compcommand = new CompositeCommand();
            compcommand.Run();
        }

    }
}
