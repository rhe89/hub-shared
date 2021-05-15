using System.Threading.Tasks;

namespace Hub.HostedServices.Commands.Core
{
    public interface ICommandWithConsumers : ICommand
    { 
        Task NotifyConsumers();
        string QueueName { get; }
    }
}