using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;

namespace Hub.Shared.Storage.ServiceBus
{
    [UsedImplicitly]
    public class MessageSender : IMessageSender
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly string _connectionString;

        public MessageSender(IConfiguration configuration, TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
            _connectionString = configuration.GetValue<string>("SERVICE_BUS_QUEUE_SAS");
        }
        
        public async Task AddToQueue(string queueName)
        {
            await using var client = new ServiceBusClient(_connectionString);
            
            var operation = _telemetryClient.StartOperation<DependencyTelemetry>("enqueue " + queueName);
    
            operation.Telemetry.Type = "Azure Service Bus";
            operation.Telemetry.Data = "Enqueue " + queueName;
            
            var sender = client.CreateSender(queueName);

            var message = new ServiceBusMessage();
            
            message.ApplicationProperties.Add("ParentId", operation.Telemetry.Id);
            message.ApplicationProperties.Add("RootId", operation.Telemetry.Context.Operation.Id);
                
            await sender.SendMessageAsync(message);
        }
    }
}