using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;

namespace Hub.Shared.Storage.Repository.Core
{
    [UsedImplicitly]
    public interface IHubDbRepository 
    {
        [UsedImplicitly]
        public IList<TDto> All<TEntity, TDto>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        public Task<IList<TDto>> AllAsync<TEntity, TDto>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        public IList<TDto> Where<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        public Task<IList<TDto>> WhereAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        public TDto First<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        public Task<TDto> FirstAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        public TDto Single<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        public Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        public TDto FirstOrDefault<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        public Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        void QueueAdd<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        TDto Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        Task<TDto> AddAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        void QueueUpdate<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        Task UpdateAsync<TEntity, TDto>(TDto tDto)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
                [UsedImplicitly]
        void QueueRemove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        void QueueRemove<TEntity>(TEntity tEntity)
            where TEntity : EntityBase;
        
        [UsedImplicitly]
        void Remove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        [UsedImplicitly]
        Task RemoveAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        [UsedImplicitly]
        void ExecuteQueue();
        
        [UsedImplicitly]
        Task ExecuteQueueAsync();
        
        [UsedImplicitly]
        TDestination Map<TSource, TDestination>(TSource source) 
            where TDestination : class;

        [UsedImplicitly]
        IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source);

        [UsedImplicitly]
        Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source);
    }
}