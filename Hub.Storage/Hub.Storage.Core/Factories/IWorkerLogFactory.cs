using System.Threading.Tasks;

namespace Hub.Storage.Core.Factories
{
    public interface IWorkerLogFactory
    {
        Task AddWorkerLog(string name, bool success, string errorMessage, string initiatedBy);
        Task DeleteDueWorkerLogs();
        Task DeleteDueWorkerLogs(int ageInDaysOfLogsToDelete);
    }
}