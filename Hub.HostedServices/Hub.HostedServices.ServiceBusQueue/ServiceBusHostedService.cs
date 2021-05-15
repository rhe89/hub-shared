using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Hub.HostedServices.Commands.Logging.Core;
using Hub.HostedServices.Core;
using Hub.HostedServices.ServiceBusQueue.Commands;
using Hub.ServiceBus.Core;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.ServiceBusQueue
{
    public abstract class ServiceBusHostedService : HostedServiceBase
    {
        private readonly IServiceBusQueueCommand _serviceBusQueueCommand;
        private readonly IQueueProcessor _queueProcessor;

        protected ServiceBusHostedService(ILogger<ServiceBusHostedService> logger, 
            ICommandLogFactory commandLogFactory, 
            IServiceBusQueueCommand serviceBusQueueCommand,
            IQueueProcessor queueProcessor) : base(logger, commandLogFactory)
        {
            _serviceBusQueueCommand = serviceBusQueueCommand;
            _queueProcessor = queueProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation($"Initializing queue processor for queue {_serviceBusQueueCommand.QueueName}");

            await _queueProcessor.Start(_serviceBusQueueCommand.QueueName, Handle);
            
            Logger.LogInformation("Queue processor initialized");
        }
        
        private async Task Handle(ProcessMessageEventArgs args)  
        {  
            if (args.Message == null)  
                throw new ArgumentNullException(nameof(args.Message));

            var body = Encoding.Default.GetString(args.Message.Body);
            
            Logger.LogInformation($"New message in queue {_serviceBusQueueCommand.QueueName} created at {args.Message.EnqueuedTime:dd.MM.yyyy HH.mm.ss} received: {body}");
            
            await base.ExecuteAsync(_serviceBusQueueCommand, args.CancellationToken);

            await args.CompleteMessageAsync(args.Message);
        }
    }
}