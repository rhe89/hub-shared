using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Hub.HostedServices.Tasks
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue, IDisposable
    {
        private readonly ConcurrentQueue<IBackgroundTask> _workItems;
        private readonly SemaphoreSlim _signal;

        public BackgroundTaskQueue()
        {
            _workItems = new ConcurrentQueue<IBackgroundTask>();
            _signal = new SemaphoreSlim(0);
        }
        
        public void QueueBackgroundTask(IBackgroundTask backgroundTask)
        {
            if (backgroundTask == null)
            {
                throw new ArgumentNullException(nameof(backgroundTask));
            }

            backgroundTask.ManualExecution = true;
            
            _workItems.Enqueue(backgroundTask);
            
            _signal.Release();
        }

        public async Task<IBackgroundTask> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

        public void Dispose()
        {
            _signal?.Dispose();
        }
    }
}