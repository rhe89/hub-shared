using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
{
    public class DbRepository<TDbContext> : DbRepositoryBase<TDbContext>
        where TDbContext : DbContext 
    {
        public DbRepository(TDbContext dbContext) : base(dbContext) {}
    }
}