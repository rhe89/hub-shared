using System.Threading.Tasks;

namespace Hub.Storage.Factories
{
    public interface ISettingFactory
    {
        Task UpdateSetting(string key, string value);
    }
}