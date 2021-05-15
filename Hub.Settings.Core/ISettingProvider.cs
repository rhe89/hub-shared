using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hub.Settings.Core
{
    public interface ISettingProvider
    {
        Task<IList<SettingDto>> GetSettings();
        T GetSetting<T>(string key);
    }
}