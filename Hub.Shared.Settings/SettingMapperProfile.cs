using AutoMapper;
using JetBrains.Annotations;

namespace Hub.Shared.Settings;

[UsedImplicitly]
public class SettingMapperProfile : Profile
{
    public SettingMapperProfile()
    {
        CreateMap<Setting, SettingDto>()
            .ReverseMap();
    }
}