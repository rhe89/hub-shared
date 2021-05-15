using System.Collections;
using System.Collections.Generic;

namespace Hub.HostedServices.Schedule.Commands
{
    public class ScheduledCommandCollection : IScheduledCommandCollection
    {
        private readonly IEnumerable<IScheduledCommand> _scheduledCommands;

        public ScheduledCommandCollection(IEnumerable<IScheduledCommand> scheduledCommands)
        {
            _scheduledCommands = scheduledCommands;
        }
        
        public IEnumerator<IScheduledCommand> GetEnumerator()
        {
            return _scheduledCommands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}