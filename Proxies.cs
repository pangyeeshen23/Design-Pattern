using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPattern.Proxy;

namespace DesignPattern
{
    class Proxies
    {
        public void Run()
        {
            //ProtectionProxy proxy = new ProtectionProxy();
            //proxy.Run();

            //PropertyProxy propertyProxy = new PropertyProxy();
            //propertyProxy.Run();

            ValueProxy valueProxy = new ValueProxy();
            valueProxy.Run();
        }
    }
}
