using AutoMapper;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Commands;

[UsedImplicitly]
public class CommandConfigurationProfile : Profile
{
    public CommandConfigurationProfile()
    {
        CreateMap<CommandConfiguration, CommandConfigurationDto>()
            .ReverseMap();
    }
}