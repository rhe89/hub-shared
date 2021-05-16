using System.Threading;
using System.Threading.Tasks;

namespace Hub.HostedServices.ServiceBusQueue.Commands
{
    public abstract class ServiceBusQueueCommand : IServiceBusQueueCommand
    {
        public abstract Task Execute(CancellationToken cancellationToken);
        public string Name => GetType().FullName;
        public abstract string QueueName { get; }
    }
}