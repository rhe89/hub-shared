using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hub.Storage.Entities;

namespace Hub.Storage.Repository
{
    public interface IDbRepository
    {
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes)
            where TEntity : EntityBase;
        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes)
            where TEntity : EntityBase;
        IEnumerable<TEntity> GetMany<TEntity>(params string[] includes)
            where TEntity : EntityBase;
        IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> filter, params string[] includes)
            where TEntity : EntityBase;
        Task<IList<TEntity>> GetManyAsync<TEntity>(params string[] includes) where TEntity : EntityBase;
        Task<IList<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> filter,
            params string[] includes) where TEntity : EntityBase;
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
        void SaveChanges(bool saveChanges);
        Task SaveChangesAsync(bool saveChanges);
    }
}