using System.Threading;
using System.Threading.Tasks;

namespace Hub.Shared.HostedServices.Commands
{
    public interface ICommandWithConsumers : ICommand
    { 
        Task NotifyConsumers();
        string QueueName { get; }
    }
    
    public interface ICommand
    {
        Task Execute(CancellationToken cancellationToken);
        string Name { get; }
    }
}