using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Entities
{
    public static class ModelBuilderExtensions
    {
        public static void AddBackgroundTaskConfigurationEntity(this ModelBuilder builder)
        {
            builder.Entity<BackgroundTaskConfiguration>()
                .ToTable(schema: "dbo", name: "BackgroundTaskConfiguration");
        }
        
        public static void AddSettingEntity(this ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .ToTable(schema: "dbo", name: "Setting");
        }
        
        public static void AddWorkerLogEntity(this ModelBuilder builder)
        {
            builder.Entity<WorkerLog>()
                .ToTable(schema: "dbo", name: "WorkerLog");
        }
    }
}