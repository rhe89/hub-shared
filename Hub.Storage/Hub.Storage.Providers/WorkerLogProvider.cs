using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Providers;
using Hub.Storage.Core.Repository;

namespace Hub.Storage.Providers
{
    public class WorkerLogProvider : IWorkerLogProvider
    {
        private readonly IHubDbRepository _dbRepository;

        public WorkerLogProvider(IHubDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public async Task<IEnumerable<WorkerLogDto>> GetLogs(int days)
        {
            var workerLogs = _dbRepository
                .Where<WorkerLog>(workerLog => workerLog.CreatedDate > DateTime.Now.AddDays(-days));

            return await _dbRepository.ProjectAsync<WorkerLog, WorkerLogDto>(workerLogs);
        }
    }
}