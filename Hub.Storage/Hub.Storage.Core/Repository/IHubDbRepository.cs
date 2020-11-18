using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Core.Repository
{
    public interface IHubDbRepository : IDisposable
    {
        public IList<TDto> All<TEntity, TDto>()
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public Task<IList<TDto>> AllAsync<TEntity, TDto>()
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;
        
        IQueryable<TEntity> Set<TEntity>() 
            where TEntity : EntityBase;
        
        public TDto First<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        public Task<TDto> FirstAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public TDto Single<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public TDto FirstOrDefault<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        public Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        TEntity Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void BulkAdd<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        Task BulkAddAsync<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void BulkUpdate<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void Remove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void BulkRemove<TEntity, TDto>(IEnumerable<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void BulkRemove<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : EntityBase;

        void SaveChanges();
        Task SaveChangesAsync();
        
        TDestination Map<TSource, TDestination>(TSource source) 
            where TDestination : class;

        IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source);

        Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source);

        void ToggleDispose(bool dispose);
    }
}