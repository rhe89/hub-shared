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

            await _dbRepository.AddAsync<WorkerLog, WorkerLogDto>(workerLog);
        }
        
        public async Task DeleteDueWorkerLogs()
        {
            await DeleteDueWorkerLogs(30);
        }
        
        public async Task DeleteDueWorkerLogs(int ageInDaysOfLogsToDelete)
        {
            var workerLogsToDelete = await _dbRepository
                .Where<WorkerLog>(wl => wl.CreatedDate < DateTime.Today.AddDays(-ageInDaysOfLogsToDelete))
                .ToListAsync();
            
            _logger.LogInformation($"Found {workerLogsToDelete.Count()} log items to delete");

            if (!workerLogsToDelete.Any())
            {
                return;
            }
            
            workerLogsToDelete.ForEach(workerLogToDelete => _dbRepository.QueueRemove(workerLogToDelete));

            await _dbRepository.ExecuteQueueAsync();
        }
    }
}