using System.Threading.Tasks;

namespace Hub.Settings.Core
{
    public interface ISettingFactory
    {
        Task UpdateSetting(string key, string value);
    }
}