using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Commands
{
    [UsedImplicitly]
    public interface ICommandWithConsumers : ICommand
    { 
        [UsedImplicitly]
        Task NotifyConsumers();
    }
    
    public interface ICommand
    {
        [UsedImplicitly]
        Task Execute([UsedImplicitly]CancellationToken cancellationToken);
        
        [UsedImplicitly]
        string Name { get; }
    }
}