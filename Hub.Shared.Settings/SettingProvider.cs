using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.Settings;

public interface ISettingProvider
{
    [UsedImplicitly]
    Task<IList<SettingDto>> GetSettings();

    [UsedImplicitly]
    Task<SettingDto> GetSetting(string key);
}
    
[UsedImplicitly]
public class SettingProvider : ISettingProvider
{
    private readonly IHubDbRepository _dbRepository;

    public SettingProvider(IHubDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    
    public async Task<IList<SettingDto>> GetSettings()
    {
        var settings = await _dbRepository.GetAsync<Setting, SettingDto>(GetQueryable(new SettingQuery()));

        return settings;
    }
    
    public async Task<SettingDto> GetSetting(string key)
    {
        var setting = (await _dbRepository.GetAsync<Setting, SettingDto>(GetQueryable(new SettingQuery { Key = key }))).Single();
            
        if (setting == null)
        {
            throw new ArgumentException($"Invalid settings key: {key}");
        }

        return setting;
    }
      
    private static Queryable<Setting> GetQueryable(SettingQuery settingQuery)
    {
        return new Queryable<Setting>
        {
            Where = setting => string.IsNullOrEmpty(settingQuery.Key) || setting.Key == settingQuery.Key,
            Take = settingQuery.Take,
            Skip = settingQuery.Skip
        };
    }
}