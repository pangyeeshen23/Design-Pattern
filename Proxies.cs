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

            //ValueProxy valueProxy = new ValueProxy();
            //valueProxy.Run();

            //CompositeProxy compositeProxy = new CompositeProxy();
            //compositeProxy.Run();

            //CompositeProxyArray compositeProxyArray = new CompositeProxyArray();
            //compositeProxyArray.Run();

            //CompositeProxyArray compositeProxyArray = new CompositeProxyArray();
            //compositeProxyArray.Run();

            //DynamicProxy dynamicProxy = new DynamicProxy();
            //dynamicProxy.Run();

            BitFragging bitFragging = new BitFragging();
            bitFragging.Run();
        }
    }
}
