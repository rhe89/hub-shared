using System.Threading.Tasks;

namespace Hub.Storage.Core.Factories
{
    public interface ISettingFactory
    {
        Task UpdateSetting(string key, string value);
    }
}