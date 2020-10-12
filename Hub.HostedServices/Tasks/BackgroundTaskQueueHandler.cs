using Microsoft.Extensions.DependencyInjection;

namespace Hub.HostedServices.Tasks
{
    public class BackgroundTaskQueueHandler : IBackgroundTaskQueueHandler
    {
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundTaskQueueHandler(IBackgroundTaskQueue backgroundTaskQueue,
            IServiceScopeFactory serviceScopeFactory)
        {
            _backgroundTaskQueue = backgroundTaskQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public void QueueBackgroundTask<TBackgroundTask>() where TBackgroundTask : BackgroundTask
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var scopedServices = scope.ServiceProvider;
            var task = scopedServices.GetService<TBackgroundTask>();

            _backgroundTaskQueue.QueueBackgroundTask(task);
        }
    }
}