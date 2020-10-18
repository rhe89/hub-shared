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
    
        public IEnumerable<TEntity> GetMany<TEntity>(params string[] includes) where TEntity : EntityBase
        {
            return GetMany<TEntity>(null, includes);
        }
        
        public IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));
            
            return filter == null ? query : query.Where(filter); 
        }

        public async Task<IList<TEntity>> GetManyAsync<TEntity>(params string[] includes) where TEntity : EntityBase
        {
            return await GetManyAsync<TEntity>(null, includes);
        }

        public async Task<IList<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (queryable, path) => queryable.Include(path));
            
            return filter == null ? await query.ToListAsync() : await query.Where(filter).ToListAsync();
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            return Add(entity, false);
        }

        public TEntity Add<TEntity>(TEntity entity, bool saveChanges)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            SaveChanges(saveChanges);

            return entity;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            return await AddAsync(entity, false);
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, bool saveChanges)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            await SaveChangesAsync(saveChanges);

            return entity;
        }

        public void BulkAdd<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase
        {
            BulkAdd(entities, false);
        }

        public void BulkAdd<TEntity>(ICollection<TEntity> entities, bool saveChanges)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            DbContext.AddRange(entities);

            SaveChanges(saveChanges);
        }

        public async Task BulkAddAsync<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase
        {
            await BulkAddAsync(entities, false);
        }

        public async Task BulkAddAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges)  where TEntity : EntityBase
        {
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            // ReSharper disable once MethodHasAsyncOverload
            DbContext.AddRange(entities);

            await SaveChangesAsync(saveChanges);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Update(entity, false);
        }

        public void Update<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase
        {
            entity.UpdatedDate = DateTime.Now;
            
            DbContext.Update(entity);

            SaveChanges(saveChanges);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            await UpdateAsync(entity, false);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase
        {
            entity.UpdatedDate = DateTime.Now;

            DbContext.Update(entity);

            await SaveChangesAsync(saveChanges);
        }

        public void BulkUpdate<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase
        {
            BulkUpdate(entities, false);
        }

        public void BulkUpdate<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase
        {
            var now = DateTime.Now;
            
            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            SaveChanges(saveChanges);
        }

        public async Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase
        {
            await BulkUpdateAsync(entities, false);
        }

        public async Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase
        {
            var now = DateTime.Now;
            
            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            await SaveChangesAsync(saveChanges);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            Remove(entity, false);
        }

        public void Remove<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase
        {
            DbContext.Remove(entity);

            SaveChanges(saveChanges);
        }

        public async Task RemoveAsync<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            await RemoveAsync(entity, false);
        }

        public async Task RemoveAsync<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase
        {
            DbContext.Remove(entity);

            await SaveChangesAsync(saveChanges);
        }

        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            BulkRemove(entities, false);
        }

        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities, bool saveChanges) where TEntity : EntityBase
        {
            DbContext.RemoveRange(entities);
            SaveChanges(saveChanges);
        }

        public void SaveChanges()
        {
            SaveChanges(true);
        }
        
        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync(true);
        }

        public void SaveChanges(bool saveChanges)
        {
            if (!saveChanges)
            {
                return;
            }
            
            DbContext.ChangeTracker.DetectChanges();
            DbContext.SaveChanges();
        }
        
        public async Task SaveChangesAsync(bool saveChanges)
        {
            if (!saveChanges)
            {
                return;
            }
            
            DbContext.ChangeTracker.DetectChanges();
            await DbContext.SaveChangesAsync();
        }
    }
}