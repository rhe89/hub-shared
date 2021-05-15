using Hub.HostedServices.Commands.Core;

namespace Hub.HostedServices.ServiceBusQueue.Commands
{
    public interface IServiceBusQueueCommand : ICommand
    {
        string QueueName { get; }
    }
}