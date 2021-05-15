using System;
using System.Threading.Tasks;
using Hub.HostedServices.Commands.Core;

namespace Hub.HostedServices.Schedule.Commands
{
    public interface IScheduledCommand : ICommand
    {
        DateTime LastRun { get; }
        bool IsDue { get; }
        string NextScheduledRun { get; }
        RunInterval RunInterval { get; }
        Task UpdateLastRun(DateTime lastRun);
    }
}