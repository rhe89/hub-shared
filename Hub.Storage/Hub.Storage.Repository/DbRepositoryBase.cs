using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hub.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
{
    public class DbRepositoryBase<TDbContext> : IDbRepository
        where TDbContext : DbContext
    {
        protected readonly TDbContext DbContext;

        protected DbRepositoryBase(TDbContext dbContext)
        {
            DbContext = dbContext;
            
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));

            return query.FirstOrDefault(filter);
        }
        
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));

            return await query.FirstOrDefaultAsync(filter);
        }
    
        public IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> filter = null, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));
            
            return filter == null ? query : query.Where(filter);
        }
        
        public async Task<IList<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));
            
            return filter == null ? await query.ToListAsync() : await query.Where(filter).ToListAsync();
        }
        
        public TEntity Add<TEntity>(TEntity entity, bool saveChanges = false)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            if (saveChanges) 
                SaveChanges();

            return entity;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, bool saveChanges = false)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            if (saveChanges)
                await SaveChangesAsync();

            return entity;
        }
        
        public void BulkAdd<TEntity>(ICollection<TEntity> entities, bool saveChanges = false)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            DbContext.AddRange(entities);

            if (saveChanges)
                SaveChanges();
        }
        
        public async Task BulkAddAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges = false)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            // ReSharper disable once MethodHasAsyncOverload
            DbContext.AddRange(entities);

            if (saveChanges)
                await SaveChangesAsync();
        }
        
        public void Update<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase
        {
            entity.UpdatedDate = DateTime.Now;
            
            DbContext.Update(entity);

            if (saveChanges)
                SaveChanges();
        }
        
        public async Task UpdateAsync<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase
        {
            entity.UpdatedDate = DateTime.Now;

            DbContext.Update(entity);

            if (saveChanges)
                await SaveChangesAsync();
        }
        
        public void BulkUpdate<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase
        {
            var now = DateTime.Now;
            
            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            if (saveChanges)
                SaveChanges();
        }
        
        public async Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase
        {
            var now = DateTime.Now;
            
            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            if (saveChanges)
                await SaveChangesAsync();
        }
        
        public void Remove<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase
        {
            DbContext.Remove(entity);

            if (saveChanges)
                SaveChanges();
        }
        
        public async Task RemoveAsync<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase
        {
            DbContext.Remove(entity);

            if (saveChanges)
                await SaveChangesAsync();
        }
        
        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase
        {
            DbContext.RemoveRange(entities);

            if (saveChanges)
                SaveChanges();
        }

        public void SaveChanges()
        {
            DbContext.ChangeTracker.DetectChanges();
            DbContext.SaveChanges();
        }
        
        public async Task SaveChangesAsync()
        {
            DbContext.ChangeTracker.DetectChanges();
            await DbContext.SaveChangesAsync();
        }
    }
}