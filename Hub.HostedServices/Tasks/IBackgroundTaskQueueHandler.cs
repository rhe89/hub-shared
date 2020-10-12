namespace Hub.HostedServices.Tasks
{
    public interface IBackgroundTaskQueueHandler
    {
        void QueueBackgroundTask<TBackgroundTask>() where TBackgroundTask : BackgroundTask;
    }
}