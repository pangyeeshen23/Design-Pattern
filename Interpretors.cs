using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Interpreter;

namespace DesignPattern
{
    public class Interpretors
    {
        public void Run()
        {
            Interpretor interpretor = new Interpretor();
            interpretor.Run();
        }
    }
}
