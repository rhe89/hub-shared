using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.Storage.ServiceBus
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly ILogger<QueueProcessor> _logger;
        private readonly string _connectionString;

        public QueueProcessor(IConfiguration configuration,
            ILogger<QueueProcessor> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetValue<string>("SERVICE_BUS_QUEUE_SAS");
        }
        
        public async Task Start(string queueName, Func<ProcessMessageEventArgs, Task> messageHandler)
        {
            var client = new ServiceBusClient(_connectionString);

            var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += messageHandler;

            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            _logger.LogError(arg.Exception, $"Error occured when processing message with entity path {arg.EntityPath}");
            
            return Task.CompletedTask;
        }
    }
}