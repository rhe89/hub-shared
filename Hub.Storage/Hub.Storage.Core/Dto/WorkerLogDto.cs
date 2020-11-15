using AutoMapper;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Core.Dto
{
    [AutoMap(typeof(WorkerLog))]
    public class WorkerLogDto : EntityDtoBase
    {
        public string Name { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string InitiatedBy { get; set; }
    }
}