using Hub.Storage.Dto;
using Hub.Storage.Entities;

namespace Hub.Storage.Mapping
{
    public static class EntityToDtoMapper
    {
        public static BackgroundTaskConfigurationDto Map(BackgroundTaskConfiguration backgroundTaskConfiguration)
        {
            return new BackgroundTaskConfigurationDto
            {
                Name = backgroundTaskConfiguration.Name,
                LastRun = backgroundTaskConfiguration.LastRun,
                RunIntervalType = (RunIntervalType)backgroundTaskConfiguration.RunIntervalType 
            };
        }
        
        public static SettingDto Map(Setting setting)
        {    
            return new SettingDto
            {
                Key = setting.Key,
                Value = setting.Value
            };
        }
        
        public static WorkerLogDto Map(WorkerLog workerLog)
        {
            return new WorkerLogDto
            {
                Name = workerLog.Name,
                Success = workerLog.Success,
                ErrorMessage = workerLog.ErrorMessage,
                CreatedDate = workerLog.CreatedDate
            };
        }
        
        
    }
}