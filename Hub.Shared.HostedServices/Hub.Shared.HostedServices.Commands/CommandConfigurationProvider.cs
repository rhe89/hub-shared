using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Commands;

public interface ICommandConfigurationProvider
{
    [UsedImplicitly]
    CommandConfigurationDto Get(string name);
        
    [UsedImplicitly]
    Task<IList<CommandConfigurationDto>> GetCommandConfigurations();
}
    
public class CommandConfigurationProvider : ICommandConfigurationProvider
{
    private readonly IHubDbRepository _dbRepository;

    public CommandConfigurationProvider(IHubDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
        
    public CommandConfigurationDto Get(string name)
    {
        var commandConfiguration = _dbRepository.FirstOrDefault<CommandConfiguration, CommandConfigurationDto>(GetQueryable(new CommandConfigurationQuery { Name = name }));

        return commandConfiguration;
    }

    public async Task<IList<CommandConfigurationDto>> GetCommandConfigurations()
    {
        var commandConfigurations = await _dbRepository
            .GetAsync<CommandConfiguration, CommandConfigurationDto>(GetQueryable(new CommandConfigurationQuery()));

        return commandConfigurations;
    }
    
    private static Queryable<CommandConfiguration> GetQueryable(CommandConfigurationQuery commandConfigurationQuery)
    {
        return new Queryable<CommandConfiguration>
        {
            Query = commandConfigurationQuery,
            Where = commandConfiguration => string.IsNullOrEmpty(commandConfigurationQuery.Name) || commandConfiguration.Name == commandConfigurationQuery.Name
        };
    }
}