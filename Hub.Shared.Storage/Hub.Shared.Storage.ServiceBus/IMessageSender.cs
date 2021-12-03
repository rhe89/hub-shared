using System.Threading.Tasks;

namespace Hub.Shared.Storage.ServiceBus
{
    public interface IMessageSender
    {
        Task AddToQueue(string queueName);
    }
}