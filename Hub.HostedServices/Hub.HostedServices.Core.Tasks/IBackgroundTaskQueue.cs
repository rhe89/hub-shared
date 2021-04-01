using System.Threading;
using System.Threading.Tasks;

namespace Hub.HostedServices.Core.Tasks
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundTask(IBackgroundTask backgroundTask);
        Task<IBackgroundTask> DequeueAsync(CancellationToken cancellationToken);
    }
}