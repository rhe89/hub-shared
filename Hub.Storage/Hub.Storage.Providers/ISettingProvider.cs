using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Dto;

namespace Hub.Storage.Providers
{
    public interface ISettingProvider
    {
        Task<IList<SettingDto>> GetSettings();
        T GetSetting<T>(string key);
    }
}