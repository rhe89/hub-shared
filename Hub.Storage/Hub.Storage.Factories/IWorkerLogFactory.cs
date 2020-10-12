using System.Threading.Tasks;

namespace Hub.Storage.Factories
{
    public interface IWorkerLogFactory
    {
        Task AddWorkerLog(string name, bool success, string errorMessage, string initiatedBy);
        Task DeleteDueWorkerLogs(int ageInDaysOfLogsToDelete = 30);
    }
}