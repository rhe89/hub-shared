using AutoMapper;
using Hub.HostedServices.Commands.Configuration.Core;

namespace Hub.HostedServices.Commands.Configuration
{
    public class CommandConfigurationProfile : Profile
    {
        public CommandConfigurationProfile()
        {
            CreateMap<CommandConfiguration, CommandConfigurationDto>()
                .ReverseMap();
        }
    }
}