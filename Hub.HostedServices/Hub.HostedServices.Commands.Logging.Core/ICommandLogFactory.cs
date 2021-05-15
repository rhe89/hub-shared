using System.Threading.Tasks;

namespace Hub.HostedServices.Commands.Logging.Core
{
    public interface ICommandLogFactory
    {
        Task AddCommandLog(string commandName, bool success, string errorMessage, string initiatedBy);
    }
}