using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Tasks;
using Hub.Storage.Core.Factories;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.QueueHost
{
    public class QueuedHostedService : HostedServiceBase
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, 
            ILogger<QueuedHostedService> logger, 
            IWorkerLogFactory workerLogFactory) : base(logger, workerLogFactory)
        {
            _taskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Queued Hosted Service is starting.");
            
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(cancellationToken);

                await base.ExecuteAsync(workItem, cancellationToken);
            }

            Logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}