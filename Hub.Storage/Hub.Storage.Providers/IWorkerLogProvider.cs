using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Dto;

namespace Hub.Storage.Providers
{
    public interface IWorkerLogProvider
    {
        Task<IEnumerable<WorkerLogDto>> GetLogs(int days);
    }
}