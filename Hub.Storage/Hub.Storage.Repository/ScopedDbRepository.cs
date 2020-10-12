using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
{
    public class ScopedDbRepository<TDbContext> : DbRepositoryBase<TDbContext>, IScopedDbRepository
        where TDbContext : DbContext 
    {
        public ScopedDbRepository(TDbContext dbContext) : base(dbContext) {}
        
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}