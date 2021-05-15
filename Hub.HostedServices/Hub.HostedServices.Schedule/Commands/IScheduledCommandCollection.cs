using System.Collections.Generic;

namespace Hub.HostedServices.Schedule.Commands
{
    public interface IScheduledCommandCollection : IEnumerable<IScheduledCommand>
    {
    }
}