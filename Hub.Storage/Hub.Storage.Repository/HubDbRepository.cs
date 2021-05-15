using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Hub.Storage.Repository.Core;
using Hub.Storage.Repository.Dto;
using Hub.Storage.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Repository
{
    public class HubDbRepository<TDbContext> : IHubDbRepository
        where TDbContext : HubDbContext
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Queue<EntityBase> _addQueue;
        private readonly Queue<EntityBase> _updateQueue;
        private readonly Queue<EntityBase> _removeQueue;

        public HubDbRepository(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            
            _addQueue = new Queue<EntityBase>();
            _updateQueue = new Queue<EntityBase>();
            _removeQueue = new Queue<EntityBase>();
        }
        
        public IList<TDto> All<TEntity, TDto>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entities = QueryWithIncludes(dbContext, include);

            return Project<TEntity, TDto>(entities);
        }

        public async Task<IList<TDto>> AllAsync<TEntity, TDto>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entities = QueryWithIncludes(dbContext, include);
            
            return await ProjectAsync<TEntity, TDto>(entities);
        }
        
        public IList<TDto> Where<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();

            var entities = QueryWithIncludes(dbContext, include).Where(predicate);
            
            return Project<TEntity, TDto>(entities);
        }
        
        public async Task<IList<TDto>> WhereAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();

            var entities = QueryWithIncludes(dbContext, include).Where(predicate);
            
            return await ProjectAsync<TEntity, TDto>(entities);
        }
        
        public TDto First<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = QueryWithIncludes(dbContext, include).First(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = await QueryWithIncludes(dbContext, include).FirstAsync(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public TDto Single<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = QueryWithIncludes(dbContext, include).Single(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = await QueryWithIncludes(dbContext, include).SingleAsync(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public TDto FirstOrDefault<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = QueryWithIncludes(dbContext, include).FirstOrDefault(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = await QueryWithIncludes(dbContext, include).FirstOrDefaultAsync(predicate);
            
            return Map<TEntity, TDto>(entity);
        }

        private static IQueryable<TEntity> QueryWithIncludes<TEntity>(TDbContext dbContext, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) where TEntity : EntityBase
        {
            var query = dbContext.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }
           
            return query;

        }
        
        public void QueueAdd<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);
            
            var now = DateTime.Now;
            
            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            _addQueue.Enqueue(entity);
        }
        
        public TDto Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
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
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
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
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);
            
            entity.UpdatedDate = DateTime.Now;

            _updateQueue.Enqueue(entity);
        }
        
        public void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = Map<TDto, TEntity>(tDto);
            
            entity.UpdatedDate = DateTime.Now;
            
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }
        
        public async Task UpdateAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = Map<TDto, TEntity>(tDto);
            
            entity.UpdatedDate = DateTime.Now;
            
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }
        
        public void QueueRemove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
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
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = dbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }
        
        public async Task RemoveAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();
            
            var entity = dbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        
        public void ExecuteQueue()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TDbContext>();

            while (_addQueue.Count > 0)
            {
                var item = _addQueue.Dequeue();
            
                dbContext.Add(item);
            }
            
            while (_updateQueue.Count > 0)
            {
                var item = _updateQueue.Dequeue();
            
                dbContext.Update(item);
            }
            
            while (_removeQueue.Count > 0)
            {
                var item = _removeQueue.Dequeue();
            
                var entity = dbContext.Find(item.GetType(), item.Id);

                dbContext.Remove(entity);
            }

            dbContext.SaveChanges();
        }

        public async Task ExecuteQueueAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetService<TDbContext>();

            while (_addQueue.Count > 0)
            {
                var item = _addQueue.Dequeue();
            
                await dbContext.AddAsync(item);
            }
            
            while (_updateQueue.Count > 0)
            {
                var item = _updateQueue.Dequeue();
            
                dbContext.Update(item);
            }
            
            while (_removeQueue.Count > 0)
            {
                var item = _removeQueue.Dequeue();
            
                var entity = await dbContext.FindAsync(item.GetType(), item.Id);

                dbContext.Remove(entity);
            }

            await dbContext.SaveChangesAsync();
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
}