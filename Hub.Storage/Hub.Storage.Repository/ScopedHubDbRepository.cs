using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;

namespace Hub.Storage.Repository
{
    public class ScopedHubDbRepository<TDbContext> : HubDbRepository<TDbContext>, IScopedHubDbRepository
        where TDbContext : HubDbContext
    {
        public ScopedHubDbRepository(TDbContext dbContext) : base(dbContext)
        {
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}