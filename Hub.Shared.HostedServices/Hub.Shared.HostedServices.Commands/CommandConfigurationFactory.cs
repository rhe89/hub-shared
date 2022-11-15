using System;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Commands;

public interface ICommandConfigurationFactory
{
    [UsedImplicitly]
    CommandConfigurationDto CreateDefaultCommandConfiguration(string name);

    [UsedImplicitly]
    Task UpdateLastRun(string name, DateTime lastRun);

    [UsedImplicitly]
    Task UpdateRunIntervalType(string name, string runInterval);

    [UsedImplicitly]
    Task DeleteConfigurations();
}

public class CommandConfigurationFactory : ICommandConfigurationFactory
{
    private readonly ICommandConfigurationProvider _commandConfigurationProvider;
    private readonly IHubDbRepository _dbRepository;

    public CommandConfigurationFactory(
        ICommandConfigurationProvider commandConfigurationProvider,
        IHubDbRepository dbRepository)
    {
        _commandConfigurationProvider = commandConfigurationProvider;
        _dbRepository = dbRepository;
    }

    public CommandConfigurationDto CreateDefaultCommandConfiguration(string name)
    {
        return _dbRepository.Add<CommandConfiguration, CommandConfigurationDto>(
            new CommandConfigurationDto
            {
                Name = name,
                LastRun = DateTime.MinValue,
                RunInterval = RunInterval.Hour.ToString()
            });
    }

    public async Task UpdateLastRun(string name, DateTime lastRun)
    {
        var commandConfiguration = _commandConfigurationProvider.Get(name);

        if (commandConfiguration == null)
        {
            return;
        }

        commandConfiguration.LastRun = lastRun;

        await _dbRepository.UpdateAsync<CommandConfiguration, CommandConfigurationDto>(commandConfiguration);
    }

    public async Task UpdateRunIntervalType(string name, string runInterval)
    {
        var commandConfiguration = _commandConfigurationProvider.Get(name);

        if (commandConfiguration == null)
        {
            return;
        }

        commandConfiguration.RunInterval = runInterval;

        await _dbRepository.UpdateAsync<CommandConfiguration, CommandConfigurationDto>(commandConfiguration);
    }

    public async Task DeleteConfigurations()
    {
        var commandConfigurations = await _commandConfigurationProvider.GetCommandConfigurations();

        foreach (var commandConfiguration in commandConfigurations)
        {
            _dbRepository.QueueRemove<CommandConfiguration, CommandConfigurationDto>(commandConfiguration);
        }

        await _dbRepository.ExecuteQueueAsync();
    }
}