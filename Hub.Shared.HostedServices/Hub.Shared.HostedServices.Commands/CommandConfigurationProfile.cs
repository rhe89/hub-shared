using AutoMapper;

namespace Hub.Shared.HostedServices.Commands
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