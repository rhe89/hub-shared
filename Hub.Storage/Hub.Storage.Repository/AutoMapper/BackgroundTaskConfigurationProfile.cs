using AutoMapper;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Repository.AutoMapper
{
    public class BackgroundTaskConfigurationProfile : Profile
    {
        public BackgroundTaskConfigurationProfile()
        {
            CreateMap<BackgroundTaskConfiguration, BackgroundTaskConfigurationDto>()
                .ReverseMap();
        }
    }
}