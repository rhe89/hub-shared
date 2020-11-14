using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;

namespace Hub.Storage.Core.Providers
{
    public interface IWorkerLogProvider
    {
        Task<IEnumerable<WorkerLogDto>> GetLogs(int days);
    }
}