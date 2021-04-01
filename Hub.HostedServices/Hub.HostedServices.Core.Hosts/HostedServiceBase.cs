using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Core.Tasks;
using Hub.Storage.Core.Factories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.Core.Hosts
{
    public abstract class HostedServiceBase : BackgroundService
    {
        protected readonly ILogger<HostedServiceBase> Logger;
        private readonly IWorkerLogFactory _workerLogFactory;

        protected HostedServiceBase(ILogger<HostedServiceBase> logger, 
            IWorkerLogFactory workerLogFactory)
        {
            Logger = logger;
            _workerLogFactory = workerLogFactory;
        }

        protected async Task ExecuteAsync(IBackgroundTask backgroundTask,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"{backgroundTask.Name} starting.");

                await backgroundTask.Execute(cancellationToken);

                await SaveWorkerLog(true, backgroundTask.Name);

            }
            catch (Exception exception)
            {
                Logger.LogError(exception, $"Error occurred executing {backgroundTask.Name}.");
                await SaveWorkerLog(false, backgroundTask.Name, exception);
            }
            
            Logger.LogInformation($"{backgroundTask.Name} finished.");

            if (backgroundTask.ManualExecution)
            {
                return;
            }
            
            await backgroundTask.UpdateLastRun(DateTime.Now);
            
            Logger.LogInformation($"{backgroundTask.Name}'s next run: {backgroundTask.NextScheduledRun}");
        }
            
        private async Task SaveWorkerLog(bool success, string taskName, Exception exception = null)
        {
            try
            {
                var errorMessage = exception == null
                        ? ""
                        : $"Exception: {exception.Message}. Inner exception: {exception.InnerException}. Stacktrace: {exception.StackTrace}";

                var initiatedBy = GetType().Name;

                await _workerLogFactory.AddWorkerLog(taskName, success, errorMessage, initiatedBy);
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error occured when saving WorkerLog with name {taskName} in {GetType().Name}");
            }
        }

    }
}
