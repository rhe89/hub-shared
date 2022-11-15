using System;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.Settings;

public interface ISettingFactory
{
    [UsedImplicitly]
    Task UpdateSetting(string key, string value);
}
    
[UsedImplicitly]
public class SettingFactory : ISettingFactory
{
    private readonly ISettingProvider _settingProvider;
    private readonly IHubDbRepository _dbRepository;

    public SettingFactory(ISettingProvider settingProvider, IHubDbRepository dbRepository)
    {
        _settingProvider = settingProvider;
        _dbRepository = dbRepository;
    }
        
    public async Task UpdateSetting(string key, string value)
    {
        var setting = await _settingProvider.GetSetting(key);
            
        if (setting == null) { throw new ArgumentException($"Invalid settings key: {key}");}

        setting.Value = value;

        await _dbRepository.UpdateAsync<Setting, SettingDto>(setting);
    }
}