using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Core;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.Schedule
{
    public class HostedService : HostedServiceBase
    {
        private readonly IScheduledCommandCollection _scheduledCommandCollection;
        private Task _executingTask;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        public HostedService(ILogger<HostedService> logger, 
            IScheduledCommandCollection scheduledCommandCollection,
            IConfiguration configuration,
            TelemetryClient telemetryClient) : base(logger, configuration, telemetryClient)
        {
            _scheduledCommandCollection = scheduledCommandCollection;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_scheduledCommandCollection.Any())
                {
                    throw new HostedServicesException("No scheduled commands registered");
                }
                foreach (var scheduledCommand in _scheduledCommandCollection)
                {
                    if (scheduledCommand == null)
                    {
                        continue;
                    }
                    
                    if (!scheduledCommand.IsDue)
                    {
                        Logger.LogInformation($"{scheduledCommand.Name} not due yet. Next scheduled run: {scheduledCommand.NextScheduledRun}");
                        continue;
                    }

                    _executingTask = ExecuteAsync(scheduledCommand, cancellationToken);
                    
                    await _executingTask;
                    
                    await scheduledCommand.UpdateLastRun(DateTime.Now);
            
                    Logger.LogInformation($"{scheduledCommand.Name}'s next run: {scheduledCommand.NextScheduledRun}");
                }
                
                var delayMillis = TimeSpan.FromMinutes(1);
                
                Logger.LogInformation($"Awaiting {delayMillis.Minutes} minutes for next iteration");
                
                await Task.Delay(delayMillis, cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping timer hosted service");

            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _cancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public override void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
