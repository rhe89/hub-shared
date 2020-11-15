using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;
using Hub.Storage.Core.Providers;
using Hub.Storage.Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Storage.Providers
{
    public class WorkerLogProvider : IWorkerLogProvider
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WorkerLogProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public async Task<IEnumerable<WorkerLogDto>> GetLogs(int days)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            using var scopedDbRepository = scope.ServiceProvider.GetService<IScopedHubDbRepository>();

            var workerLogs = scopedDbRepository
                .Where<WorkerLog>(workerLog => workerLog.CreatedDate > DateTime.Now.AddDays(-days));

            return await scopedDbRepository.ProjectAsync<WorkerLog, WorkerLogDto>(workerLogs);
        }
    }
}