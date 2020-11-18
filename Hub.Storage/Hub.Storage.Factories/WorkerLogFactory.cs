using System;
using System.Linq;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hub.Storage.Factories
{
    public class WorkerLogFactory : IWorkerLogFactory
    {
        private readonly IHubDbRepository _dbRepository;
        private readonly ILogger<WorkerLogFactory> _logger;

        public WorkerLogFactory(IHubDbRepository dbRepository, ILoggerFactory loggerFactory)
        {
            _dbRepository = dbRepository;
            _logger = loggerFactory.CreateLogger<WorkerLogFactory>();
        }
        public async Task AddWorkerLog(string name, bool success, string errorMessage, string initiatedBy)
        {
            var workerLog = new WorkerLogDto
            {
                Name = name,
                Success = success,
                ErrorMessage = errorMessage,
                InitiatedBy = initiatedBy
            };

            _dbRepository.Add<WorkerLog, WorkerLogDto>(workerLog);

            await _dbRepository.SaveChangesAsync();
        }
        
        public async Task DeleteDueWorkerLogs()
        {
            await DeleteDueWorkerLogs(30);
        }
        
        public async Task DeleteDueWorkerLogs(int ageInDaysOfLogsToDelete)
        {
            _dbRepository.ToggleDispose(false);
            
            var workerLogsToDelete = await _dbRepository
                .Where<WorkerLog>(wl => wl.CreatedDate < DateTime.Today.AddDays(-ageInDaysOfLogsToDelete))
                .ToListAsync();
            
            _logger.LogInformation($"Found {workerLogsToDelete.Count()} log items to delete");

            if (!workerLogsToDelete.Any())
            {
                _dbRepository.ToggleDispose(true);
                _dbRepository.Dispose();
                
                return;
            }

            _dbRepository.BulkRemove(workerLogsToDelete);
            
            _dbRepository.ToggleDispose(true);

            await _dbRepository.SaveChangesAsync();
        }
    }
}