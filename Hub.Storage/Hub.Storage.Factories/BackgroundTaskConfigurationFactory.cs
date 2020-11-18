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

             _dbRepository.SaveChanges();
             
             return _dbRepository.Map<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(entity);
        }   
        
        public async Task UpdateLastRun(string name, DateTime lastRun)
        {
            _dbRepository.ToggleDispose(false);
            
            var dto = _dbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
            if (dto == null)
            {
                return;
            }

            dto.LastRun = lastRun;
            
            await Update(dto);
        }   
        
        public async Task UpdateRunIntervalType(string name, RunIntervalType runIntervalType)
        {
            _dbRepository.ToggleDispose(false);

            var dto = _dbRepository
                .FirstOrDefault<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(x => x.Name == name);
            
            if (dto == null)
            {
                return;
            }

            dto.RunIntervalType = runIntervalType;
            
            await Update(dto);
        }

        private async Task Update(BackgroundTaskConfigurationDto dto)
        {
            _dbRepository.Update<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>(dto);
            
            _dbRepository.ToggleDispose(true);

            await _dbRepository.SaveChangesAsync();
        }
    }
}