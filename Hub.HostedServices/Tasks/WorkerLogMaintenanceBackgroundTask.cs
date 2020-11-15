using System.Threading;
using System.Threading.Tasks;
using Hub.Storage.Factories;
using Hub.Storage.Providers;

namespace Hub.HostedServices.Tasks
{
    public class WorkerLogMaintenanceBackgroundTask : BackgroundTask
    {
        private readonly IWorkerLogFactory _workerLogFactory;

        public WorkerLogMaintenanceBackgroundTask(IBackgroundTaskConfigurationProvider backgroundTaskConfigurationProvider,
            IBackgroundTaskConfigurationFactory backgroundTaskConfigurationFactory,
            IWorkerLogFactory workerLogFactory) : base(backgroundTaskConfigurationProvider, backgroundTaskConfigurationFactory)
        {
            _workerLogFactory = workerLogFactory;
        }

        public override async Task Execute(CancellationToken cancellationToken)
        {
            await _workerLogFactory.DeleteDueWorkerLogs();
        }
    }
}