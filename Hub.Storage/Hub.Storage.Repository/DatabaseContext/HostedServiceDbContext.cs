using Hub.Storage.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository.DatabaseContext
{
    public class HostedServiceDbContext : HubDbContext
    {
        public HostedServiceDbContext(DbContextOptions<HubDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<WorkerLog>()
                .ToTable(schema: "dbo", name: "WorkerLog");
            
            builder.Entity<BackgroundTaskConfiguration>()
                .ToTable(schema: "dbo", name: "BackgroundTaskConfiguration");
        }
    }
}