using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;

namespace Hub.HostedServices.Tasks
{
    public interface IBackgroundTask
    {
        Task Execute(CancellationToken cancellationToken);
        Task UpdateLastRun(DateTime lastRun);    
        string Name { get; }
        DateTime LastRun { get; }
        RunIntervalType RunIntervalType { get; }
        bool IsDue { get; }
        string NextScheduledRun { get; }
        bool ManualExecution { get; set; }
    }
}