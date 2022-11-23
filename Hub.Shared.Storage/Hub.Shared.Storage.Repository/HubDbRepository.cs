using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hub.Shared.Storage.Repository.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Storage.Repository;

public class HubDbRepository<TDbContext> : IHubDbRepository
    where TDbContext : HubDbContext
{
    protected readonly IServiceScopeFactory ServiceScopeFactory;
    private readonly IMapper _mapper;
    private readonly Queue<EntityBase> _addQueue;
    private readonly Queue<EntityBase> _updateQueue;
    private readonly Queue<EntityBase> _removeQueue;

    public HubDbRepository(IMapper mapper,
                           IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        _mapper = mapper;

        _addQueue = new Queue<EntityBase>();
        _updateQueue = new Queue<EntityBase>();
        _removeQueue = new Queue<EntityBase>();
    }

    public async Task<IList<TDto>> GetAsync<TEntity, TDto>(Queryable<TEntity> queryable)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        IQueryable<TEntity> dbQueryable = dbContext
            .Set<TEntity>()
            .Where(queryable.Where);
        
        foreach (var include in queryable.Includes)
        {
            dbQueryable = dbQueryable.Include(include);
        }

        var filtered = GetQueryable(queryable, dbQueryable);

        return await ProjectAsync<TEntity, TDto>(filtered);
    }

    protected IQueryable<TEntity> GetQueryable<TEntity>(Queryable<TEntity> queryable, IQueryable<TEntity> dbQueryable) where TEntity : EntityBase
    {
        if (queryable.OrderBy != null)
        {
            dbQueryable = dbQueryable
                .OrderBy(queryable.OrderBy);
        }
        else if (queryable.OrderByDescending != null)
        {
            var orderedDbQueryable = dbQueryable
                .OrderByDescending(queryable.OrderByDescending);

            if (queryable.ThenOrderByDescending != null)
            {
                orderedDbQueryable = orderedDbQueryable.ThenByDescending(queryable.ThenOrderByDescending);
            }

            dbQueryable = orderedDbQueryable;
        }

        if (queryable.Take != null)
        {
            dbQueryable = dbQueryable
                .Take(queryable.Take.Value);
        }

        if (queryable.Skip != null)
        {
            dbQueryable = dbQueryable
                .Skip(queryable.Skip.Value);
        }

        return dbQueryable;
    }

    public void QueueAdd<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        var entity = Map<TDto, TEntity>(tDto);

        var now = DateTime.Now;

        entity.UpdatedDate = now;
        entity.CreatedDate = now;

        _addQueue.Enqueue(entity);
    }

    public TDto Add<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = Map<TDto, TEntity>(tDto);

        var now = DateTime.Now;

        entity.UpdatedDate = now;
        entity.CreatedDate = now;

        dbContext.Add(entity);
        dbContext.SaveChanges();

        return Map<TEntity, TDto>(entity);
    }

    public async Task<TDto> AddAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = Map<TDto, TEntity>(tDto);

        var now = DateTime.Now;

        entity.UpdatedDate = now;
        entity.CreatedDate = now;

        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return Map<TEntity, TDto>(entity);
    }

    public void QueueUpdate<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        var entity = Map<TDto, TEntity>(tDto);

        entity.UpdatedDate = DateTime.Now;

        _updateQueue.Enqueue(entity);
    }

    public void Update<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = Map<TDto, TEntity>(tDto);

        entity.UpdatedDate = DateTime.Now;

        dbContext.Update(entity);
        dbContext.SaveChanges();
    }

    public async Task UpdateAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = Map<TDto, TEntity>(tDto);

        entity.UpdatedDate = DateTime.Now;

        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public void QueueRemove<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        var entity = Map<TDto, TEntity>(tDto);

        _removeQueue.Enqueue(entity);
    }

    public void QueueRemove<TEntity>(TEntity tEntity)
        where TEntity : EntityBase
    {
        _removeQueue.Enqueue(tEntity);
    }

    public void Remove<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = dbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

        dbContext.Remove(entity);
        dbContext.SaveChanges();
    }

    public async Task RemoveAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        using var scope = ServiceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var entity = dbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public void ExecuteQueue()
    {
        using var scope = ServiceScopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        while (_addQueue.Count > 0)
        {
            var item = Dequeue(_addQueue);

            dbContext.Add(item);
        }

        while (_updateQueue.Count > 0)
        {
            var item = Dequeue(_updateQueue);

            dbContext.Update(item);
        }

        while (_removeQueue.Count > 0)
        {
            var item = Dequeue(_removeQueue);

            var entity = dbContext.Find(item.GetType(), item.Id);

            if (entity != null)
            {
                dbContext.Remove(entity);
            }
        }

        dbContext.SaveChanges();
    }

    public async Task ExecuteQueueAsync()
    {
        using var scope = ServiceScopeFactory.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        while (_addQueue.Count > 0)
        {
            var item = Dequeue(_addQueue);

            await dbContext.AddAsync(item);
        }

        while (_updateQueue.Count > 0)
        {
            var item = Dequeue(_updateQueue);

            var trackedEntry = dbContext.ChangeTracker.Entries<EntityBase>().FirstOrDefault(e => e.Entity.Id == item.Id);

            if (trackedEntry == null)
            {
                dbContext.Update(item);
            }
            else
            {
                dbContext.Entry(trackedEntry.Entity).CurrentValues.SetValues(item);
            }
        }

        while (_removeQueue.Count > 0)
        {
            var item = Dequeue(_removeQueue);

            var entity = await dbContext.FindAsync(item.GetType(), item.Id);

            if (entity != null)
            {
                dbContext.Remove(entity);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    protected virtual EntityBase Dequeue(Queue<EntityBase> queue)
    {
        var item = queue.Dequeue();

        return item;
    }

    public IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source)
    {
        var projected = _mapper.ProjectTo<TDestination>(source).ToList();

        return projected;
    }

    public async Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source)
    {
        var projected = await _mapper.ProjectTo<TDestination>(source).ToListAsync();

        return projected;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
        where TDestination : class
    {
        var mapped = source == null ? null : _mapper.Map<TDestination>(source);

        return mapped;
    }
}