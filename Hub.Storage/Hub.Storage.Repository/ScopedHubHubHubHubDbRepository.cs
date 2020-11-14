using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;

namespace Hub.Storage.Repository
{
    public class ScopedHubHubHubHubDbRepository<TDbContext> : HubHubDbRepository<TDbContext>, IScopedHubHubDbRepository
        where TDbContext : HubDbContext
    {
    public ScopedHubHubHubHubDbRepository(TDbContext dbContext) : base(dbContext)
    {
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
    }
}