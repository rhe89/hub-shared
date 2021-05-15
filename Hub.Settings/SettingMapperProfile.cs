using AutoMapper;
using Hub.Settings.Core;

namespace Hub.Settings
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