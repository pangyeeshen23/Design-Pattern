using DesignPattern.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public static class Visitors
    {
        public static void Run()
        {
            //IntrusiveVisitor.Run();

            //ReflectiveVisitor.Run();

            //ClassicVisitor.Run();

            //ReductionAndTransform.Run();

            //DynamicVisitor.Run();

            VisitorBuilders.Run();
        }
    }
}
