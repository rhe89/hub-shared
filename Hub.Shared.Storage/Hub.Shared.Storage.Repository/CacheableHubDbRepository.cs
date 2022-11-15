using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hub.Shared.Storage.Repository.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Storage.Repository;

public interface ICacheableHubDbRepository : IHubDbRepository
{
}

public class CacheableHubDbRepository<TDbContext> : HubDbRepository<TDbContext>, ICacheableHubDbRepository
    where TDbContext : HubDbContext
{
    private readonly IMemoryCache _memoryCache;
    
    public CacheableHubDbRepository(IMapper mapper, IServiceScopeFactory serviceScopeFactory) : base(mapper, serviceScopeFactory)
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }
    
    protected override IQueryable<TEntity> GetQueryable<TEntity>(TDbContext dbContext, Queryable<TEntity> queryable)
    {
        if (_memoryCache.TryGetValue(typeof(TEntity).Name, out IQueryable<TEntity> entities))
        {
            return entities;
        }
        
        return base.GetQueryable(dbContext, queryable);
    }

    private void InvalidateCache<TEntity>()
    {
        InvalidateCache(typeof(TEntity).Name);
    }
    
    private void InvalidateCache(string key)
    {
        _memoryCache.Remove(key);
    }

    public new TDto Add<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        return base.Add<TEntity, TDto>(tDto);
    }
        
    public new async Task<TDto> AddAsync<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        return await base.AddAsync<TEntity, TDto>(tDto);
    }

    public new void Update<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        base.Update<TEntity, TDto>(tDto);
    }
        
    public new async Task UpdateAsync<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        await base.UpdateAsync<TEntity, TDto>(tDto);
    }

    public new void Remove<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        base.Remove<TEntity, TDto>(tDto);
    }
        
    public new async Task RemoveAsync<TEntity, TDto>(TDto tDto) 
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        InvalidateCache<TEntity>();

        await base.RemoveAsync<TEntity, TDto>(tDto);
    }

    protected override EntityBase Dequeue(Queue<EntityBase> queue)
    {
        var item = queue.Dequeue();

        InvalidateCache(item.GetType().Name);

        return item;
    }
}