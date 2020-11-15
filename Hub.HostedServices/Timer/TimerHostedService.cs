using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Tasks;
using Hub.Storage.Factories;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.Timer
{
    public class TimerHostedService : HostedServiceBase
    {
        private readonly IBackgroundTaskCollection _backgroundTaskCollection;
        private Task _executingTask;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        public TimerHostedService(ILogger<TimerHostedService> logger, 
            IWorkerLogFactory workerLogFactory,
            IBackgroundTaskCollection backgroundTaskCollection) : base(logger, workerLogFactory)
        {
            _backgroundTaskCollection = backgroundTaskCollection;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!_backgroundTaskCollection.Any())
                {
                    throw new HostedServicesException("No background tasks registered in backgroundTaskCollection");
                }
                foreach (var backgroundTask in _backgroundTaskCollection)
                {
                    if (!backgroundTask.IsDue)
                    {
                        Logger.LogInformation($"{backgroundTask.Name} not due yet. Next scheduled run: {backgroundTask.NextScheduledRun}");
                        continue;
                    }

                    _executingTask = ExecuteAsync(backgroundTask, cancellationToken);

                    await _executingTask;
                }

                var delayMillis = TimeSpan.FromMinutes(10);
                
                Logger.LogInformation($"Awaiting {delayMillis.Minutes} minutes for next iteration");
                await Task.Delay(delayMillis, cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping timer hosted service.");

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
            _cancellationTokenSource.Cancel();
        }
    }
}
