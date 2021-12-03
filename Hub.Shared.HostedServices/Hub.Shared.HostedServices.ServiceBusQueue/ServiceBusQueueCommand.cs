using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;

namespace Hub.Shared.HostedServices.ServiceBusQueue
{
    public interface IServiceBusQueueCommand : ICommand
    {
        string Trigger { get; }
    }
    
    public abstract class ServiceBusQueueCommand : IServiceBusQueueCommand
    {
        public abstract Task Execute(CancellationToken cancellationToken);
        public string Name => GetType().Name;
        public abstract string Trigger { get; }
    }
}