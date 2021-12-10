using Hub.Shared.HostedServices.Commands;
using Microsoft.EntityFrameworkCore;

namespace Hub.Shared.Storage.Repository
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
            builder.Entity<CommandConfiguration>()
                .ToTable(schema: "dbo", name: "CommandConfiguration");
        }
    }
}