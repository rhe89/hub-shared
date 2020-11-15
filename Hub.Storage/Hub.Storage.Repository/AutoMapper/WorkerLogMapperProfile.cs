using AutoMapper;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Repository.AutoMapperProfiles
{
    public class WorkerLogMapperProfile : Profile
    {
        public WorkerLogMapperProfile()
        {
            CreateMap<WorkerLog, WorkerLogDto>()
                .ReverseMap();
        }
    }
}