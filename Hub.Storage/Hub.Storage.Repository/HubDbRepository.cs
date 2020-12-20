using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Repository
{
    public class HubDbRepository<TDbContext> : IHubDbRepository
        where TDbContext : HubDbContext
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private TDbContext _currentScopedDbContext;
        private IServiceScope _serviceScope;
        private bool _doDispose = true;

        public HubDbRepository(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        ~HubDbRepository()
        {
            Dispose(false);
        }

        private DbContext DbContext
        {
            get
            {
                if (_currentScopedDbContext != null)
                    return _currentScopedDbContext;
                
                _serviceScope ??= _serviceScopeFactory.CreateScope();

                _currentScopedDbContext = _serviceScope.ServiceProvider.GetService<TDbContext>();
                _currentScopedDbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                _currentScopedDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                return _currentScopedDbContext;
            }
        }
        
        public void ToggleDispose(bool dispose)
        {
            _doDispose = dispose;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_doDispose)
                {
                    return;
                }
                
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                    _serviceScope = null;
                }

                if (_currentScopedDbContext != null)
                {
                    _currentScopedDbContext.Dispose();
                    _currentScopedDbContext = null;
                }
            }
        }

        public IList<TDto> All<TEntity, TDto>() 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = DbContext.Set<TEntity>();

            return Project<TEntity, TDto>(entities);
        }

        public async Task<IList<TDto>> AllAsync<TEntity, TDto>()
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = DbContext.Set<TEntity>();

            return await ProjectAsync<TEntity, TDto>(entities);
        }

        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
        {
            var entities = DbContext.Set<TEntity>().Where(predicate);
            
            return entities;
        }
        
        public IQueryable<TEntity> Set<TEntity>() 
            where TEntity : EntityBase
        {
            var entities = DbContext.Set<TEntity>();
            
            return entities;
        }
        
        public TDto First<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().First(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = await DbContext.Set<TEntity>().FirstAsync(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public TDto Single<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = DbContext.Set<TEntity>().Single(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = await DbContext.Set<TEntity>().SingleAsync(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public TDto FirstOrDefault<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = DbContext.Set<TEntity>().FirstOrDefault(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public TEntity Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);
            
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            return entity;
        }
        
        public void BulkAdd<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = Map<ICollection<TDto>, ICollection<TEntity>>(tDtos);
            
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            DbContext.AddRange(entities);        
        }


        public async Task BulkAddAsync<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = Map<ICollection<TDto>, ICollection<TEntity>>(tDtos);

            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            await DbContext.AddRangeAsync(entities);        
        }

        public void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);
            
            entity.UpdatedDate = DateTime.Now;
            
            DbContext.Update(entity);        
        }

        public void BulkUpdate<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var now = DateTime.Now;
            
            var entities = Project<TDto, TEntity>(tDtos.AsQueryable());
            
            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);        
        }

        public void Remove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            DbContext.Remove(entity);
        }

        public void BulkRemove<TEntity, TDto>(IEnumerable<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = DbContext.Set<TEntity>().Where(x => tDtos.Any(y => y.Id == x.Id));

            DbContext.RemoveRange(entities);        
        }

        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : EntityBase
        {
            DbContext.RemoveRange(entities);
        }
        
        public void SaveChanges()
        {
            
            DbContext.ChangeTracker.DetectChanges();
            DbContext.SaveChanges();
            
            Dispose();
        }
        
        public async Task SaveChangesAsync()
        {
            DbContext.ChangeTracker.DetectChanges();
            
            await DbContext.SaveChangesAsync();
            
            Dispose();
        }

        public IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source) 
        {
            var projected = _mapper.ProjectTo<TDestination>(source).ToList();
            
            Dispose();

            return projected;
        }
        
        public async Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source) 
        {
            var projected = await _mapper.ProjectTo<TDestination>(source).ToListAsync();
            
            Dispose();

            return projected;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class
        {
            var mapped = source == null ? null : _mapper.Map<TDestination>(source);
            
            Dispose();

            return mapped;
        }
    }
}