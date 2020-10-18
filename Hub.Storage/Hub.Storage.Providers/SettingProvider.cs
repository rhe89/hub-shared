using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Entities;
using Hub.Storage.Mapping;
using Hub.Storage.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Providers
{
    public class SettingProvider : ISettingProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SettingProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
    
        public async Task<IList<SettingDto>> GetSettings()
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var settings = await dbRepository.GetManyAsync<Setting>();

            return settings.Select(EntityToDtoMapper.Map).ToList();
        }
        
        public T GetSetting<T>(string key)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var setting = dbRepository.GetSingle<Setting>(x => x.Key == key);
            
            if (setting == null)
            {
                throw new ArgumentException($"Invalid settings key: {key}");
            }

            var value = setting.Value;

            var t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;

            return value == null ? default : (T)Convert.ChangeType(value, t);
        }
        
        
    }
}