using System.Threading;
using System.Threading.Tasks;

namespace Hub.HostedServices.Commands.Core
{
    public interface ICommand
    {
        Task Execute(CancellationToken cancellationToken);
        string Name { get; }
    }
}