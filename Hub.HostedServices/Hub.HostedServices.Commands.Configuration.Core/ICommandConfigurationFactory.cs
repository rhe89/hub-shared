using System;
using System.Threading.Tasks;

namespace Hub.HostedServices.Commands.Configuration.Core
{
    public interface ICommandConfigurationFactory
    {
        CommandConfigurationDto CreateDefaultCommandConfiguration(string name);
        Task UpdateLastRun(string name, DateTime lastRun);
        Task UpdateRunIntervalType(string name, string runInterval);
        Task DeleteConfigurations();
    }
}