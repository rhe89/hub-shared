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
        IEnumerable<TEntity> GetMany<TEntity>(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
            where TEntity : EntityBase;
        Task<IList<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            params string[] includes) where TEntity : EntityBase;
        TEntity Add<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        Task<TEntity> AddAsync<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        void BulkAdd<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase;
        Task BulkAddAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase;
        void Update<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        Task UpdateAsync<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        void BulkUpdate<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase;
        Task BulkUpdateAsync<TEntity>(ICollection<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase;
        void Remove<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        Task RemoveAsync<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : EntityBase;
        void BulkRemove<TEntity>(IEnumerable<TEntity> entities, bool saveChanges = false) where TEntity : EntityBase;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}