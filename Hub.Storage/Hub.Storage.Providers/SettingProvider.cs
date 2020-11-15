using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Providers;
using Hub.Storage.Core.Repository;
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

            using var dbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var settings = await dbRepository.AllAsync<Setting, SettingDto>();

            return settings;
        }
        
        public T GetSetting<T>(string key)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var setting = dbRepository.Single<Setting, SettingDto>(x => x.Key == key);
            
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