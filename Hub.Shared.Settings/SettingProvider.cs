using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.Settings
{
    public interface ISettingProvider
    {
        Task<IList<SettingDto>> GetSettings();
        T GetSetting<T>(string key);
    }
    
    public class SettingProvider : ISettingProvider
    {
        private readonly IHubDbRepository _dbRepository;

        public SettingProvider(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
    
        public async Task<IList<SettingDto>> GetSettings()
        {
            var settings = await _dbRepository.AllAsync<Setting, SettingDto>();

            return settings;
        }
        
        public T GetSetting<T>(string key)
        {
            var setting = _dbRepository.Single<Setting, SettingDto>(x => x.Key == key);
            
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