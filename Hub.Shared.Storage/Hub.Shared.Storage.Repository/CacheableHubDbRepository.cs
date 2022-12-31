using AutoMapper;
using Hub.Shared.Storage.Repository.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Storage.Repository;

public interface ICacheableHubDbRepository : IHubDbRepository
{
}

public class CacheableHubDbRepository<TDbContext> : HubDbRepository<TDbContext>, ICacheableHubDbRepository
    where TDbContext : HubDbContext
{
    public CacheableHubDbRepository(IMapper mapper, IServiceScopeFactory serviceScopeFactory) : base(mapper, serviceScopeFactory)
    {
    }
}