using System;
using System.Threading.Tasks;
using Hub.Storage.Entities;
using Hub.Storage.Repository;
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

            using var dbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var setting = dbRepository.GetSingle<Setting>(x => x.Key == key);
            
            if (setting == null) { throw new ArgumentException($"Invalid settings key: {key}");}

            setting.Value = value;

            await dbRepository.SaveChangesAsync();
        }
    }
}