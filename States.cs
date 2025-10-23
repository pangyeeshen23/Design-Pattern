using DesignPattern.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class States
    {
        public static void Run()
        {
            ClassicImplementation classic = new ClassicImplementation();
            classic.Run();
        }
    }
}
