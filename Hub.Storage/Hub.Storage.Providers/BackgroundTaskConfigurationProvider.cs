using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Entities;
using Hub.Storage.Mapping;
using Hub.Storage.Repository;
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

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();
            
            var backgroundTaskConfiguration = scopedDbRepository
                .GetSingle<BackgroundTaskConfiguration>(bt => bt.Name == name);

            if (backgroundTaskConfiguration == null)
                return null;
            
            return EntityToDtoMapper.Map(backgroundTaskConfiguration);
        }

        public async Task<IList<BackgroundTaskConfigurationDto>> GetBackgroundTaskConfigurations()
        {
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();
            
            var backgroundTaskConfigurations = await scopedDbRepository
                .GetManyAsync<BackgroundTaskConfiguration>();

            return backgroundTaskConfigurations.Select(EntityToDtoMapper.Map).ToList();
        }
    }
}