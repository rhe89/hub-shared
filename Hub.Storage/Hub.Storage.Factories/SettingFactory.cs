using System;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Repository;

namespace Hub.Storage.Factories
{
    public class SettingFactory : ISettingFactory
    {
        private readonly IHubDbRepository _dbRepository;

        public SettingFactory(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public async Task UpdateSetting(string key, string value)
        {
            var setting = _dbRepository.Single<Setting, SettingDto>(x => x.Key == key);
            
            if (setting == null) { throw new ArgumentException($"Invalid settings key: {key}");}

            setting.Value = value;

            await _dbRepository.UpdateAsync<Setting, SettingDto>(setting);
        }
    }
}