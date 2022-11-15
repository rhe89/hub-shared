using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Hub.Shared.Storage.ServiceBus;

public interface IMessageSender
{
    [UsedImplicitly]
    Task AddToQueue(string queueName);

    [UsedImplicitly]
    Task AddToQueue(string queueName, object messageBody);
}