using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Hub.Storage.Repository
{
    public class HubDbRepository<TDbContext> : IHubDbRepository
        where TDbContext : HubDbContext
    {
        protected readonly TDbContext DbContext;
        private readonly IMapper _mapper;

        public HubDbRepository(TDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public IList<TDto> All<TEntity, TDto>() 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = DbContext.Set<TEntity>();

            return Project<TEntity, TDto>(entities);
        }

        public async Task<IList<TDto>> AllAsync<TEntity, TDto>()
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = DbContext.Set<TEntity>();

            return await ProjectAsync<TEntity, TDto>(entities);
        }

        public IQueryable<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
        {
            var entities = DbContext.Set<TEntity>().Where(predicate);
            
            return entities;
        }
        
        public TDto First<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().First(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = await DbContext.Set<TEntity>().FirstAsync(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public TDto Single<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = DbContext.Set<TEntity>().Single(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = await DbContext.Set<TEntity>().SingleAsync(predicate);

            return Map<TEntity, TDto>(entity);
        }
        
        public TDto FirstOrDefault<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = DbContext.Set<TEntity>().FirstOrDefault(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public async Task<TDto> FirstOrDefaultAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        { 
            var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            
            return Map<TEntity, TDto>(entity);
        }
        
        public TDto Add<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            return Add<TEntity, TDto>(tDto, false);
        }
        
        public TDto Add<TEntity, TDto>(TDto tDto, bool saveChanges)
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);
            
            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            SaveChanges(saveChanges);

            return tDto;
        }

        public async Task<TDto> AddAsync<TEntity, TDto>(TDto tDto)
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            return await AddAsync<TEntity, TDto>(tDto, false);
        }

        public async Task<TDto> AddAsync<TEntity, TDto>(TDto tDto, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = Map<TDto, TEntity>(tDto);

            var now = DateTime.Now;

            entity.UpdatedDate = now;
            entity.CreatedDate = now;
            
            DbContext.Add(entity);

            await SaveChangesAsync(saveChanges);

            return Map<TEntity, TDto>(entity);
        }
        
        

        public void BulkAdd<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            BulkAdd<TEntity, TDto>(tDtos, false);
        }

        public void BulkAdd<TEntity, TDto>(ICollection<TDto> tDtos, bool saveChanges)  
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = Map<ICollection<TDto>, ICollection<TEntity>>(tDtos);
            
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            DbContext.AddRange(entities);

            SaveChanges(saveChanges);
        }

        public async Task BulkAddAsync<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            await BulkAddAsync<TEntity, TDto>(tDtos, false);
        }

        public async Task BulkAddAsync<TEntity, TDto>(ICollection<TDto> tDtos, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entities = Map<ICollection<TDto>, ICollection<TEntity>>(tDtos);

            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
                entity.CreatedDate = now;
            }
            
            await DbContext.AddRangeAsync(entities);

            await SaveChangesAsync(saveChanges);
        }

        public void Update<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            Update<TEntity, TDto>(tDto, false);
        }

        public void Update<TEntity, TDto>(TDto tDto, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            entity.UpdatedDate = DateTime.Now;
            
            DbContext.Update(entity);

            SaveChanges(saveChanges);
        }

        public async Task UpdateAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            await UpdateAsync<TEntity, TDto>(tDto, false);
        }

        public async Task UpdateAsync<TEntity, TDto>(TDto tDto, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            entity.UpdatedDate = DateTime.Now;

            DbContext.Update(entity);

            await SaveChangesAsync(saveChanges);
        }

        public void BulkUpdate<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            BulkUpdate<TEntity, TDto>(tDtos, false);
        }

        public void BulkUpdate<TEntity, TDto>(ICollection<TDto> tDtos, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var now = DateTime.Now;
            
            var entities = DbContext.Set<TEntity>().Where(x => tDtos.Any(y => y.Id == x.Id));

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            SaveChanges(saveChanges);
        }

        public async Task BulkUpdateAsync<TEntity, TDto>(ICollection<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            await BulkUpdateAsync<TEntity, TDto>(tDtos, false);
        }

        public async Task BulkUpdateAsync<TEntity, TDto>(ICollection<TDto> tDtos, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var now = DateTime.Now;
            
            var entities = DbContext.Set<TEntity>().Where(x => tDtos.Any(y => y.Id == x.Id));

            foreach (var entity in entities)
            {
                entity.UpdatedDate = now;
            }
            
            DbContext.UpdateRange(entities);

            await SaveChangesAsync(saveChanges);
        }

        public void Remove<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            Remove<TEntity, TDto>(tDto, false);
        }

        public void Remove<TEntity, TDto>(TDto tDto, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            DbContext.Remove(entity);

            SaveChanges(saveChanges);
        }

        public async Task RemoveAsync<TEntity, TDto>(TDto tDto) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            await RemoveAsync<TEntity, TDto>(tDto, false);
        }

        public async Task RemoveAsync<TEntity, TDto>(TDto tDto, bool saveChanges) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == tDto.Id);

            DbContext.Remove(entity);

            await SaveChangesAsync(saveChanges);
        }

        public void BulkRemove<TEntity, TDto>(IEnumerable<TDto> tDtos) 
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            BulkRemove<TEntity, TDto>(tDtos, false);
        }

        public void BulkRemove<TEntity, TDto>(IEnumerable<TDto> tDtos, bool saveChanges) 
            where TDto : EntityDtoBase 
            where TEntity : EntityBase
        {
            var entities = DbContext.Set<TEntity>().Where(x => tDtos.Any(y => y.Id == x.Id));

            DbContext.RemoveRange(entities);
            
            SaveChanges(saveChanges);
        }
        
        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : EntityBase
        {
            DbContext.RemoveRange(entities);
            
            SaveChanges(false);
        }
        
        public void BulkRemove<TEntity>(IEnumerable<TEntity> entities, bool saveChanges) 
            where TEntity : EntityBase
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
        
        public IList<TDestination> Project<TSource, TDestination>(IQueryable<TSource> source) 
        {
            return _mapper.ProjectTo<TDestination>(source).ToList();
        }
        
        public async Task<IList<TDestination>> ProjectAsync<TSource, TDestination>(IQueryable<TSource> source) 
        {
            return await _mapper.ProjectTo<TDestination>(source).ToListAsync();
        }
        
        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : class
        {
            return source == null ? null : _mapper.Map<TDestination>(source);
        }
    }
}