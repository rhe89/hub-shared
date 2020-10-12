using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hub.HostedServices.Tasks
{
    public class BackgroundTaskCollection : IBackgroundTaskCollection
    {
        private readonly IEnumerable<IBackgroundTask> _backgroundTasks;

        public BackgroundTaskCollection(IEnumerable<IBackgroundTask> backgroundTasks)
        {
            _backgroundTasks = backgroundTasks.ToList();
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