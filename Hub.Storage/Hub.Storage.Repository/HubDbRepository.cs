using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hub.Storage.Core;
using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
{
    public abstract class HubDbRepository<TDbContext> : IHubDbRepository
        where TDbContext : HubDbContext
    {
        protected readonly TDbContext DbContext;

        protected HubDbRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public IList<TEntity> All<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().ToList();
        public Task<List<TEntity>> AllAsync<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().ToListAsync();
        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().Where(predicate);
        public TEntity First<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().First();
        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().First(predicate);
        public Task<TEntity> FirstAsync<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().FirstAsync();
        public Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().FirstAsync(predicate);
        public TEntity Single<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().Single();
        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().Single(predicate);
        public Task<TEntity> SingleAsync<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().SingleAsync();
        public Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().SingleAsync(predicate);
        public TEntity FirstOrDefault<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().FirstOrDefault();
        public TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().FirstOrDefault(predicate);
        public Task<TEntity> FirstOrDefaultAsync<TEntity>() where TEntity : EntityBase => DbContext.Set<TEntity>().FirstOrDefaultAsync();
        public Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase => DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

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
            
            await DbContext.AddRangeAsync(entities);

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

        public void SaveChanges(bool doSaveChanges)
        {
            if (!doSaveChanges)
            {
                return;
            }
            
            DbContext.ChangeTracker.DetectChanges();
            DbContext.SaveChanges();
        }
        
        public async Task SaveChangesAsync(bool doSaveChanges)
        {
            if (!doSaveChanges)
            {
                return;
            }
            
            DbContext.ChangeTracker.DetectChanges();
            await DbContext.SaveChangesAsync();
        }
    }
}