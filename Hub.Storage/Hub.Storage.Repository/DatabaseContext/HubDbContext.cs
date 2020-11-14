using Microsoft.EntityFrameworkCore;
using Setting = Hub.Storage.Repository.Entities.Setting;

namespace Hub.Storage.Repository.DatabaseContext
{
    public class HubDbContext : DbContext
    {
        public HubDbContext(DbContextOptions<HubDbContext> options) : base(options) { }

        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .ToTable(schema: "dbo", name: "Setting");
        }
    }
}