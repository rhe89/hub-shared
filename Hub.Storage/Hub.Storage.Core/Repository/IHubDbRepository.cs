using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Core.Repository
{
    public interface IHubDbRepository 
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
        
        void QueueAdd<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        TEntity Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        Task<TEntity> AddAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void QueueUpdate<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        Task UpdateAsync<TEntity, TDto>(TDto tDto)
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        void QueueRemove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void QueueRemove<TEntity>(TEntity tEntity)
            where TEntity : EntityBase;
        
        void Remove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;
        
        Task RemoveAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase;

        void ExecuteQueue();
        
        Task ExecuteQueueAsync();
        
        TDestination Map<TSource, TDestination>(TSource source) 
            where TDestination : class;

        IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source);

        Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source);
    }
}