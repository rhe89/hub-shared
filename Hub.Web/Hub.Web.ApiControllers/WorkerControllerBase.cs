using System.Threading.Tasks;
using Hub.Storage.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.ApiControllers
{
    public class WorkerControllerBase : ControllerBase
    {
        protected readonly IWorkerLogProvider WorkerLogProvider;

        public WorkerControllerBase(IWorkerLogProvider workerLogProvider)
        {
            WorkerLogProvider = workerLogProvider;
        }
        
        [HttpGet("logs")]
        public async Task<IActionResult> Logs(int days)
        {
            var workerLogs = await WorkerLogProvider.GetLogs(days);
            
            return Ok(workerLogs);
        }
    }
}