using Autofac;
using JetBrains.Annotations;
using MediatR;

namespace DesignPattern.Mediators
{
    // ping command

    public class PingCommand : IRequest<PongResponse>
    {

    }

    public class PongResponse
    {
        public DateTime TimeStamp { get; set; }
        public PongResponse(DateTime timestamp)
        {
            TimeStamp = timestamp;
        }
    }

    [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow))
                .ConfigureAwait(false);
        }
    }

    public class MediatorR
    {
        public void Run()
        {
            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            //builder.Register<ServiceFactory>(ctx =>
            //{
            //    var c = ctx.Resolve<IComponentContext>();
            //    return t => c.Resolve(t);
            //});
            //builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            //    .AsImplementedInterfaces();
            //var container = builder.Build();
            //var mediator = container.Resolve<IMediator>();
            //var response = mediator.Send(new PingCommand()).Result;
            //Console.WriteLine($"We got a response at {response.TimeStamp}");
        }
    }
}
