using System.Threading.Tasks;
using Hub.Storage.Core.Providers;
using Hub.Storage.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.Api.Controllers
{
    public abstract class WorkerControllerBase : Controller
    {
        protected readonly IWorkerLogProvider WorkerLogProvider;

        protected WorkerControllerBase(IWorkerLogProvider workerLogProvider)
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