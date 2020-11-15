using System;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Repository;
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
            var backgroundTaskConfiguration = new BackgroundTaskConfigurationDto
            {
                Name = name,
                RunIntervalType = RunIntervalType.Day,
                LastRun = DateTime.MinValue
            };
            
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            return scopedDbRepository.Add<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(backgroundTaskConfiguration, true);
        }   
        
        public async Task UpdateLastRun(string name, DateTime lastRun)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var backgroundTaskConfiguration = scopedDbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
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
            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var backgroundTaskConfiguration = scopedDbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.RunIntervalType = runIntervalType;
            
            await Update(backgroundTaskConfiguration);
        }

        private async Task Update(BackgroundTaskConfigurationDto backgroundTaskConfiguration)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            await scopedDbRepository.UpdateAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(backgroundTaskConfiguration, true);
        }
    }
}