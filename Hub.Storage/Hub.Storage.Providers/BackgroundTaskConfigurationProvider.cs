using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Providers;
using Hub.Storage.Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Providers
{
    public class BackgroundTaskConfigurationProvider : IBackgroundTaskConfigurationProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundTaskConfigurationProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public BackgroundTaskConfigurationDto Get(string name)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();
            
            var backgroundTaskConfiguration = scopedDbRepository
                .Single<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(bt => bt.Name == name);

            return backgroundTaskConfiguration;
        }

        public async Task<IList<BackgroundTaskConfigurationDto>> GetBackgroundTaskConfigurations()
        {
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var backgroundTaskConfigurations = await scopedDbRepository
                .AllAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>();

            return backgroundTaskConfigurations;
        }
    }
}