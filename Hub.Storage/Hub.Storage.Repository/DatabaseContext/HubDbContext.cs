using Microsoft.EntityFrameworkCore;
using Setting = Hub.Storage.Core.Entities.Setting;

namespace Hub.Storage.Repository.DatabaseContext
{
    public class HubDbContext : DbContext
    {
        public HubDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .ToTable(schema: "dbo", name: "Setting");
        }
    }
}