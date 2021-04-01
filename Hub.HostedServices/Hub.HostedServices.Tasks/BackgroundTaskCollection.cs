using System.Collections;
using System.Collections.Generic;
using Hub.HostedServices.Core.Tasks;

namespace Hub.HostedServices.Tasks
{
    public class BackgroundTaskCollection : IBackgroundTaskCollection
    {
        private readonly IEnumerable<IBackgroundTask> _backgroundTasks;

        public BackgroundTaskCollection(IEnumerable<IBackgroundTask> backgroundTasks)
        {
            _backgroundTasks = backgroundTasks;
        }
        
        public IEnumerator<IBackgroundTask> GetEnumerator()
        {
            return _backgroundTasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}