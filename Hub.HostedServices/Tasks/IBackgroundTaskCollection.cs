using System.Collections.Generic;

namespace Hub.HostedServices.Tasks
{
    public interface IBackgroundTaskCollection : IEnumerable<IBackgroundTask>
    {
    }
}