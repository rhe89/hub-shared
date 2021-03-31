using Microsoft.EntityFrameworkCore;
using Setting = Hub.Storage.Core.Entities.Setting;

namespace Hub.Storage.Repository.DatabaseContext
{
    public class HubDbContext : DbContext
    {
        public HubDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .ToTable(schema: "dbo", name: "Setting");
        }
    }
}