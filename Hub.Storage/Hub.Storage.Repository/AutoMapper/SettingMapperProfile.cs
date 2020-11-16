using AutoMapper;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Repository.AutoMapper
{
    public class SettingMapperProfile : Profile
    {
        public SettingMapperProfile()
        {
            CreateMap<Setting, SettingDto>()
                .ReverseMap();
        }
    }
}