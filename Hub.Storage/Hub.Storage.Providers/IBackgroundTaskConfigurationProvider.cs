using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Dto;

namespace Hub.Storage.Providers
{
    public interface IBackgroundTaskConfigurationProvider
    {
        BackgroundTaskConfigurationDto Get(string name);
        Task<IList<BackgroundTaskConfigurationDto>> GetBackgroundTaskConfigurations();
    }
}