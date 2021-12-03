using AutoMapper;

namespace Hub.Shared.Settings
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