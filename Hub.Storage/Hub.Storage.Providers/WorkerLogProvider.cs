using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Entities;
using Hub.Storage.Repository;
using Hub.Storage.Mapping;
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
            var scope = _serviceScopeFactory.CreateScope();

            var scopedDbRepository = scope.ServiceProvider.GetService<IScopedDbRepository>();
            
            var workerLogs = await scopedDbRepository.GetManyAsync<WorkerLog>(workerLog => workerLog.CreatedDate > DateTime.Now.AddDays(-days));

            return workerLogs.Select(EntityToDtoMapper.Map);
        }
    }
}