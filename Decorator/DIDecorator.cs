using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace DesignPattern.Decorator
{
 
    //Dependency Injection Decorator
    public class DIDecorator
    {
        public interface IReportingService
        {
            void Report();
        }

        public class ReportingService : IReportingService
        {
            public void Report()
            {
                Console.WriteLine("Here is your report");
            }
        }

        public class ReportingServiceWithLogging : IReportingService
        {
            IReportingService _reportingService;

            public ReportingServiceWithLogging(IReportingService reportingService)
            {
                _reportingService = reportingService;
            }

            public void Report()
            {
                Console.WriteLine("Commencing Log");
                _reportingService.Report();
                Console.WriteLine("Ending Log...");
            }
        }

        public void Run()
        {
            var container = new ContainerBuilder();
            container.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            container.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting"
            );

            using (var c = container.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }
        }

    }
}
