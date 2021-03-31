using System;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Repository;

namespace Hub.Storage.Factories
{
    public class BackgroundTaskConfigurationFactory : IBackgroundTaskConfigurationFactory
    {
        private readonly IHubDbRepository _dbRepository;

        public BackgroundTaskConfigurationFactory(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public BackgroundTaskConfigurationDto CreateDefaultBackgroundTaskConfiguration(string name)
        {
            var dto = new BackgroundTaskConfigurationDto
            {
                Name = name,
                RunIntervalType = RunIntervalType.Day,
                LastRun = DateTime.MinValue
            };
            
            var entity = _dbRepository.Add<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(dto);
            
             return _dbRepository.Map<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(entity);
        }   
        
        public async Task UpdateLastRun(string name, DateTime lastRun)
        {
            var backgroundTaskConfiguration = _dbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.LastRun = lastRun;
            
            await _dbRepository.UpdateAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(backgroundTaskConfiguration);        }   
        
        public async Task UpdateRunIntervalType(string name, RunIntervalType runIntervalType)
        {
            var backgroundTaskConfiguration = await _dbRepository
                .FirstOrDefaultAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
            if (backgroundTaskConfiguration == null)
            {
                return;
            }

            backgroundTaskConfiguration.RunIntervalType = runIntervalType;
            
            await _dbRepository.UpdateAsync<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(backgroundTaskConfiguration);
        }
    }
}