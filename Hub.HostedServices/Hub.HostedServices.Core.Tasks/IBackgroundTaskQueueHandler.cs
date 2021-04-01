namespace Hub.HostedServices.Core.Tasks
{
    public interface IBackgroundTaskQueueHandler
    {
        void QueueBackgroundTask<TBackgroundTask>() where TBackgroundTask : IBackgroundTask;
    }
}