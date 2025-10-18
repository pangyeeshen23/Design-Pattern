using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Observer
{
    public class EventSubscriptions
    {
        public interface IEvent
        {
            event EventHandler SomethingHappened;
        }

        public interface ISend<TEvent> where TEvent : IEvent
        {
            event EventHandler<TEvent> Sender;
        }

        public interface IHandle<TEvent> where TEvent : IEvent
        {
            void Handle(object sender, TEvent e);
        }

        public class ButtonPressedEvent : IEvent
        {
            public int NumberOfClicks;

            public event EventHandler SomethingHappened;
        }

        public class Button : ISend<ButtonPressedEvent>
        {
            public event EventHandler<ButtonPressedEvent> Sender;
            public void Fire(int clicks)
            {
                Sender?.Invoke(this, new ButtonPressedEvent()
                {
                    NumberOfClicks = clicks
                });
            }
        }

        public class Logging : IHandle<ButtonPressedEvent>
        {
            public void Handle(object sender, ButtonPressedEvent args)
            {
                Console.WriteLine($"Button clicked {args.NumberOfClicks} times");
            }
        }

        public static class Program
        {
            public static void Run()
            {
                var cb = new ContainerBuilder();
                var ass = Assembly.GetExecutingAssembly();
                cb.RegisterAssemblyTypes(ass)
                   .AsClosedTypesOf(typeof(ISend<>))
                   .SingleInstance();
                cb.RegisterAssemblyTypes(ass)
                  .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>)))
                  .OnActivated(act =>
                  {
                      var instanceType = act.Instance.GetType();
                      var interfaces = instanceType.GetInterfaces();
                      foreach (var i in interfaces)
                      {
                          if(i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandle<>))
                          {
                              var arg0 = i.GetGenericArguments()[0];
                              var senderType = typeof(ISend<>).MakeGenericType(arg0);
                              var allSenderType = typeof(IEnumerable<>).MakeGenericType(senderType);
                              var allServices = act.Context.Resolve(allSenderType);
                              foreach(var service in (IEnumerable) allServices)
                              {
                                  var eventInfo = service.GetType().GetEvent("Sender");
                                  var handleMethod = instanceType.GetMethod("Handle");
                                  var handlerDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, null, handleMethod);
                                  eventInfo.AddEventHandler(service, handlerDelegate);
                              }
                          }
                      }
                  })
                  .SingleInstance()
                  .AsSelf();
                var c = cb.Build();
                var button = c.Resolve<Button>();
                var logging = c.Resolve<Logging>();
                button.Fire(1);
                button.Fire(2);
            }
        }
    }
}
