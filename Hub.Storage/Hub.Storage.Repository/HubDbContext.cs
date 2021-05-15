using Hub.HostedServices.Commands.Configuration.Core;
using Hub.Settings.Core;
using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
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
                
            builder.Entity<CommandConfiguration>()
                .ToTable(schema: "dbo", name: "CommandConfiguration");
        }
    }
}