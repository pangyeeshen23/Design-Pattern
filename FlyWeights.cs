using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Flyweight;

namespace DesignPattern
{
    class FlyWeights
    {
        public void Run()
        {
            //DefaultFW defaultFW = new DefaultFW();
            //defaultFW.Run();

            //TextFormatFW textFormatFW = new TextFormatFW();
            //textFormatFW.Run();

            Sentence sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence);
        }
    }
}
