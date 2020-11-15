using System;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Factories
{
    public class SettingFactory : ISettingFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SettingFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public async Task UpdateSetting(string key, string value)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var setting = dbRepository.Single<Setting, SettingDto>(x => x.Key == key);
            
            if (setting == null) { throw new ArgumentException($"Invalid settings key: {key}");}

            setting.Value = value;

            await dbRepository.UpdateAsync<Setting, SettingDto>(setting);

            await dbRepository.SaveChangesAsync();
        }
    }
}