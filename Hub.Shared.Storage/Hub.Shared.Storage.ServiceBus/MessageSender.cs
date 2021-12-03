using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Hub.Shared.Storage.ServiceBus
{
    public class MessageSender : IMessageSender
    {
        private readonly string _connectionString;

        public MessageSender(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("SERVICE_BUS_QUEUE_SAS");
        }
        
        public async Task AddToQueue(string queueName)
        {
            await using var client = new ServiceBusClient(_connectionString);
            
            var sender = client.CreateSender(queueName);

            var message = new ServiceBusMessage();
                
            await sender.SendMessageAsync(message);
        }
    }
}