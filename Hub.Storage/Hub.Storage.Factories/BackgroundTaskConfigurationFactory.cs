using System;
using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Entities;
using Hub.Storage.Mapping;
using Hub.Storage.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Factories
{
    public class BackgroundTaskConfigurationFactory : IBackgroundTaskConfigurationFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundTaskConfigurationFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public BackgroundTaskConfigurationDto CreateDefaultBackgroundTaskConfiguration(string name)
        {
            var backgroundTaskConfiguration = new BackgroundTaskConfiguration
            {
                Name = name,
                RunIntervalType = (int)RunIntervalType.Day,
                LastRun = DateTime.MinValue
            };
            
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            scopedDbRepository.Add(backgroundTaskConfiguration, true);

            return EntityToDtoMapper.Map(backgroundTaskConfiguration);
        }   
        
        public async Task UpdateLastRun(string name, DateTime lastRun)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var backgroundTaskConfiguration =
                scopedDbRepository.GetSingle<BackgroundTaskConfiguration>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.LastRun = lastRun;
            
            await Update(backgroundTaskConfiguration);
        }   
        
        public async Task UpdateRunIntervalType(string name, RunIntervalType runIntervalType)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var backgroundTaskConfiguration =
                scopedDbRepository.GetSingle<BackgroundTaskConfiguration>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.RunIntervalType = (int)runIntervalType;
            
            await Update(backgroundTaskConfiguration);
        }

        private async Task Update(BackgroundTaskConfiguration backgroundTaskConfiguration)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            await scopedDbRepository.UpdateAsync(backgroundTaskConfiguration, true);
        }
    }
}