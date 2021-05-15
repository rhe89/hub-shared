using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hub.HostedServices.Commands.Configuration.Core
{
    public interface ICommandConfigurationProvider
    {
        CommandConfigurationDto Get(string name);
        Task<IList<CommandConfigurationDto>> GetCommandConfigurations();
    }
}