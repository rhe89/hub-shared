using System.Threading.Tasks;

namespace Hub.ServiceBus.Core
{
    public interface IMessageSender
    {
        Task AddToQueue(string queueName);
    }
}