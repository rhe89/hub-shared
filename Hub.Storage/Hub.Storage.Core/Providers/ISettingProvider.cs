using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;

namespace Hub.Storage.Core.Providers
{
    public interface ISettingProvider
    {
        Task<IList<SettingDto>> GetSettings();
        T GetSetting<T>(string key);
    }
}