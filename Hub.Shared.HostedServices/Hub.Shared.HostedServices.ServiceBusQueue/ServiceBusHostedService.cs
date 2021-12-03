using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Hub.Shared.HostedServices.Core;
using Hub.Shared.Storage.ServiceBus;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.ServiceBusQueue
{
    public abstract class ServiceBusHostedService : HostedServiceBase
    {
        private readonly IServiceBusQueueCommand _serviceBusQueueCommand;
        private readonly IQueueProcessor _queueProcessor;

        protected ServiceBusHostedService(ILogger<ServiceBusHostedService> logger, 
            IConfiguration configuration,
            IServiceBusQueueCommand serviceBusQueueCommand,
            IQueueProcessor queueProcessor,
            TelemetryClient telemetryClient) : base(logger, configuration, telemetryClient)
        {
            _serviceBusQueueCommand = serviceBusQueueCommand;
            _queueProcessor = queueProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation($"Initializing queue processor for queue {_serviceBusQueueCommand.Trigger}");

            await _queueProcessor.Start(_serviceBusQueueCommand.Trigger, Handle);
            
            Logger.LogInformation("Queue processor initialized");
        }
        
        private async Task Handle(ProcessMessageEventArgs args)  
        {  
            if (args.Message == null)
            {
                throw new ArgumentNullException(nameof(args.Message));
            }
            
            var body = Encoding.Default.GetString(args.Message.Body);
            
            Logger.LogInformation($"New message in queue {_serviceBusQueueCommand.Trigger} created at {args.Message.EnqueuedTime:dd.MM.yyyy HH.mm.ss} received: {body}");

            await base.ExecuteAsync(_serviceBusQueueCommand, args.CancellationToken);

            await args.CompleteMessageAsync(args.Message);
        }
    }
}