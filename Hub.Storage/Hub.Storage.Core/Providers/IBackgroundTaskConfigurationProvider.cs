using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;

namespace Hub.Storage.Core.Providers
{
    public interface IBackgroundTaskConfigurationProvider
    {
        BackgroundTaskConfigurationDto Get(string name);
        Task<IList<BackgroundTaskConfigurationDto>> GetBackgroundTaskConfigurations();
    }
}