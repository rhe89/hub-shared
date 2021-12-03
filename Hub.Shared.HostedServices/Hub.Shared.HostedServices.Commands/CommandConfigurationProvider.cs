using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.HostedServices.Commands
{
    public interface ICommandConfigurationProvider
    {
        CommandConfigurationDto Get(string name);
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
            var commandConfiguration = _dbRepository
                .FirstOrDefault<CommandConfiguration, CommandConfigurationDto>(c => c.Name == name);

            return commandConfiguration;
        }

        public async Task<IList<CommandConfigurationDto>> GetCommandConfigurations()
        {
            var commandConfigurations = await _dbRepository
                .AllAsync<CommandConfiguration, CommandConfigurationDto>();

            return commandConfigurations;
        }
    }
}