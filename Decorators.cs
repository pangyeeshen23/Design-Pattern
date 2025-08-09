using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Decorator;

namespace DesignPattern
{
    class Decorators
    {
        public void Run()
        {
            //CodeBuilder codeBuilder = new CodeBuilder();
            //codeBuilder.AppendLine("class Foo");
            //codeBuilder.AppendLine("{");
            //codeBuilder.AppendLine("}");
            //Console.WriteLine(codeBuilder.ToString());

            //AdapterDecorator adapterDecorator = new AdapterDecorator();
            //adapterDecorator.Run();

            //Dragon dragon = new Dragon();
            //dragon.Fly();
            //dragon.Crawl();
            //((IWeight)dragon).GetWeight();


            //DragonTwo dragonTwo = new DragonTwo();
            //dragonTwo.Age = 5;
            //((IBird)dragonTwo).Fly();
            //((ILizard)dragonTwo).Crawl();

            //DynamicDecorator dynamicDecorator = new DynamicDecorator();
            //dynamicDecorator.RunProcess();

            DynamicDecoratorPolicy dynamicDecoratorPolicy = new DynamicDecoratorPolicy();
            dynamicDecoratorPolicy.RunProcess();
        }
    }
}
