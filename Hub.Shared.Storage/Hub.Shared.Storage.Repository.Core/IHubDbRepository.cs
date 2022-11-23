using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Hub.Shared.Storage.Repository.Core;

[UsedImplicitly]
public interface IHubDbRepository
{
    [UsedImplicitly]
    public Task<IList<TDto>> GetAsync<TEntity, TDto>(Queryable<TEntity> queryable)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    void QueueAdd<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    TDto Add<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    Task<TDto> AddAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    void QueueUpdate<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    void Update<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    Task UpdateAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    void QueueRemove<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    void QueueRemove<TEntity>(TEntity tEntity)
        where TEntity : EntityBase;

    [UsedImplicitly]
    void Remove<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

    [UsedImplicitly]
    Task RemoveAsync<TEntity, TDto>(TDto tDto)
        where TEntity : EntityBase
        where TDto : DtoBase;

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