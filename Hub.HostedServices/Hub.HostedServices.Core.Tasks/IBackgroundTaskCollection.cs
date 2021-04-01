using System.Collections.Generic;

namespace Hub.HostedServices.Core.Tasks
{
    public interface IBackgroundTaskCollection : IEnumerable<IBackgroundTask>
    {
    }
}