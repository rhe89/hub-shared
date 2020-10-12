using System;
using System.Threading.Tasks;
using Hub.Storage.Dto;

namespace Hub.Storage.Factories
{
    public interface IBackgroundTaskConfigurationFactory
    {
        BackgroundTaskConfigurationDto CreateDefaultBackgroundTaskConfiguration(string name);
        Task UpdateLastRun(string name, DateTime lastRun);
        Task UpdateRunIntervalType(string name, RunIntervalType runIntervalType);
    }
}