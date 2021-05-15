using System;
using System.Threading.Tasks;
using Hub.HostedServices.Commands.Configuration.Core;
using Hub.HostedServices.Commands.Core;
using Hub.Storage.Repository.Core;

namespace Hub.HostedServices.Commands.Configuration
{
    public class CommandConfigurationFactory : ICommandConfigurationFactory
    {
        private readonly IHubDbRepository _dbRepository;

        public CommandConfigurationFactory(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public CommandConfigurationDto CreateDefaultCommandConfiguration(string name)
        {
            var dto = new CommandConfigurationDto
            {
                Name = name,
                LastRun = DateTime.MinValue,
                RunInterval = RunInterval.Hour.ToString()
            };

            var added = _dbRepository.Add<CommandConfiguration, CommandConfigurationDto>(dto);

            return added;
        }   
        
        public async Task UpdateLastRun(string name, DateTime lastRun)
        {
            var backgroundTaskConfiguration = _dbRepository
                .FirstOrDefault<CommandConfiguration, CommandConfigurationDto>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.LastRun = lastRun;
            
            await _dbRepository.UpdateAsync<CommandConfiguration, CommandConfigurationDto>(backgroundTaskConfiguration);        
        }   
        
        public async Task UpdateRunIntervalType(string name, string runInterval)
        {
            var backgroundTaskConfiguration = await _dbRepository
                .FirstOrDefaultAsync<CommandConfiguration, CommandConfigurationDto>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.RunInterval = runInterval;
            
            await _dbRepository.UpdateAsync<CommandConfiguration, CommandConfigurationDto>(backgroundTaskConfiguration);
        }

        public async Task DeleteConfigurations()
        {
            var commandConfigurations = await _dbRepository
                .AllAsync<CommandConfiguration, CommandConfigurationDto>();

            foreach (var commandConfiguration in commandConfigurations)
            {
                _dbRepository.QueueRemove<CommandConfiguration, CommandConfigurationDto>(commandConfiguration);
            }

            await _dbRepository.ExecuteQueueAsync();
        }
    }
}