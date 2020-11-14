using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hub.Storage.Core.Repository
{
    public interface IHubDbRepository
    {
        public IList<TEntity> All<TEntity>() where TEntity : EntityBase;

        public Task<List<TEntity>> AllAsync<TEntity>()
            where TEntity : EntityBase;

        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;
        public TEntity First<TEntity>() where TEntity : EntityBase;
        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;
        public Task<TEntity> FirstAsync<TEntity>() where TEntity : EntityBase;
        public Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)  where TEntity : EntityBase;
        public TEntity Single<TEntity>() where TEntity : EntityBase;
        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;
        public Task<TEntity> SingleAsync<TEntity>() where TEntity : EntityBase;
        public Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;
        public TEntity FirstOrDefault<TEntity>() where TEntity : EntityBase;
        public TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase;
        public Task<TEntity> FirstOrDefaultAsync<TEntity>() where TEntity : EntityBase;
        public Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;
        TEntity Add<TEntity>(TEntity entity) where TEntity : EntityBase;
        TEntity Add<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task<TEntity> AddAsync<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        void BulkAdd<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase;
        void BulkAdd<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase;
        Task BulkAddAsync<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase;
        Task BulkAddAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase;
        void Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Update<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task UpdateAsync<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        void BulkUpdate<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase;
        void BulkUpdate<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase;
        Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities) where TEntity : EntityBase;
        Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges) where TEntity : EntityBase;
        void Remove<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Remove<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        Task RemoveAsync<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task RemoveAsync<TEntity>(TEntity entity, bool saveChanges) where TEntity : EntityBase;
        void BulkRemove<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase;
        void BulkRemove<TEntity>(IEnumerable<TEntity> entities, bool saveChanges) where TEntity : EntityBase;
        void SaveChanges();
        Task SaveChangesAsync();
        void SaveChanges(bool doSaveChanges);
        Task SaveChangesAsync(bool doSaveChanges);
    }
}