using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Hub.Shared.HostedServices.Core;
using Hub.Shared.Storage.ServiceBus;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.ServiceBusQueue
{
    [UsedImplicitly]
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
            Logger.LogInformation("Initializing queue processor for queue {Queue}", _serviceBusQueueCommand.Trigger);

            await _queueProcessor.Start(_serviceBusQueueCommand.Trigger, Handle);
            
            Logger.LogInformation("Queue processor initialized for queue {Queue}", _serviceBusQueueCommand.Trigger);
        }
        
        private async Task Handle(ProcessMessageEventArgs args)  
        {  
            if (args.Message == null)
            {
                throw new ArgumentNullException(nameof(args.Message));
            }
            
            var requestTelemetry = new RequestTelemetry { Name = "process " + _serviceBusQueueCommand.Trigger };

            if (args.Message.ApplicationProperties.TryGetValue("RootId", out var rootId))
            {
                requestTelemetry.Context.Operation.Id = rootId.ToString();
            }
            
            if (args.Message.ApplicationProperties.TryGetValue("ParentId", out var parentId))
            {
                requestTelemetry.Context.Operation.ParentId = parentId.ToString();
            }
            
            using var operation = TelemetryClient.StartOperation(requestTelemetry);
            
            Logger.LogInformation("New message in queue {Queue} created at {Time} received", _serviceBusQueueCommand.Trigger, args.Message.EnqueuedTime.ToString("dd.MM.yyyy HH.mm.ss"));

            await base.ExecuteAsync(_serviceBusQueueCommand, args.CancellationToken, operation);

            await args.CompleteMessageAsync(args.Message);
            
            operation.Telemetry.Stop();
        }
    }
}