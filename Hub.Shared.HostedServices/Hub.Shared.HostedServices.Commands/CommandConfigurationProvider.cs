using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Commands;

public interface ICommandConfigurationProvider
{
    [UsedImplicitly]
    Task<CommandConfigurationDto> Get(string name);
        
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
        
    public async Task<CommandConfigurationDto> Get(string name)
    {
        var commandConfiguration = await _dbRepository.GetAsync<CommandConfiguration, CommandConfigurationDto>(GetQueryable(new CommandConfigurationQuery { Name = name }));

        return commandConfiguration.FirstOrDefault();
    }

    public async Task<IList<CommandConfigurationDto>> GetCommandConfigurations()
    {
        var commandConfigurations = await _dbRepository
            .GetAsync<CommandConfiguration, CommandConfigurationDto>(GetQueryable(new CommandConfigurationQuery()));

        return commandConfigurations;
    }
    
    private static Queryable<CommandConfiguration> GetQueryable(CommandConfigurationQuery query)
    {
        return new Queryable<CommandConfiguration>(query)
        {
            Where = entity => string.IsNullOrEmpty(query.Name) || entity.Name == query.Name
        };
    }
}