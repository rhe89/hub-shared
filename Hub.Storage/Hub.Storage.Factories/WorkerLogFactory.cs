using System;
using System.Linq;
using System.Threading.Tasks;
using Hub.Storage.Entities;
using Hub.Storage.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hub.Storage.Factories
{
    public class WorkerLogFactory : IWorkerLogFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<WorkerLogFactory> _logger;

        public WorkerLogFactory(IServiceScopeFactory serviceScopeFactory, ILoggerFactory loggerFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = loggerFactory.CreateLogger<WorkerLogFactory>();
        }
        public async Task AddWorkerLog(string name, bool success, string errorMessage, string initiatedBy)
        {
            var workerLog = new WorkerLog
            {
                Name = name,
                Success = success,
                ErrorMessage = errorMessage,
                InitiatedBy = initiatedBy
            };

            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            dbRepository.Add(workerLog);

            await dbRepository.SaveChangesAsync();
        }
        
        public async Task DeleteDueWorkerLogs(int ageInDaysOfLogsToDelete)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var dbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();

            var workerLogsToDelete =
                dbRepository.GetMany<WorkerLog>(wl =>
                        wl.CreatedDate < DateTime.Today.AddDays(-ageInDaysOfLogsToDelete))
                    .ToList();
            
            _logger.LogInformation($"Found {workerLogsToDelete.Count} log items to delete");

            if (!workerLogsToDelete.Any()) return;

            dbRepository.BulkRemove(workerLogsToDelete);

            await dbRepository.SaveChangesAsync();
        }
    }
}