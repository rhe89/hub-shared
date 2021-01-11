using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Providers;
using Hub.Storage.Core.Repository;

namespace Hub.Storage.Providers
{
    public class BackgroundTaskConfigurationProvider : IBackgroundTaskConfigurationProvider
    {
        private readonly IHubDbRepository _dbRepository;

        public BackgroundTaskConfigurationProvider(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public BackgroundTaskConfigurationDto Get(string name)
        {
            var backgroundTaskConfiguration = _dbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(bt => bt.Name == name);

            return backgroundTaskConfiguration;
        }

        public async Task<IList<BackgroundTaskConfigurationDto>> GetBackgroundTaskConfigurations()
        {
            var backgroundTaskConfigurations = await _dbRepository
                .AllAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>();

            return backgroundTaskConfigurations;
        }
    }
}