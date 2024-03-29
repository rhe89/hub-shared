using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.ServiceBusQueue;

public interface IServiceBusQueueCommand : ICommand
{
    public string Message { get; set; }
    string Trigger { get; }
}
    
[UsedImplicitly]
public abstract class ServiceBusQueueCommand : IServiceBusQueueCommand
{
    public abstract Task Execute(CancellationToken cancellationToken);
        
    public string Message { get; set; }
    public string Name => GetType().Name;
        
    public abstract string Trigger { get; }
}