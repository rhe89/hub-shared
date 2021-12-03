using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Shared.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandConfigurationProvider _commandConfigurationProvider;
        private readonly ICommandConfigurationFactory _commandConfigurationFactory;
        
        public CommandController(ICommandConfigurationProvider commandConfigurationProvider,
            ICommandConfigurationFactory commandConfigurationFactory)
        {
            _commandConfigurationProvider = commandConfigurationProvider;
            _commandConfigurationFactory = commandConfigurationFactory;
        }

        [HttpGet("Configurations")]
        public async Task<IActionResult> CommandConfigurations()
        {
            var commandConfigurations = await _commandConfigurationProvider.GetCommandConfigurations();
            
            return Ok(commandConfigurations);
        }
        
        [HttpDelete("DeleteConfigurations")]
        public async Task<IActionResult> DeleteConfigurations()
        {
            await _commandConfigurationFactory.DeleteConfigurations();
            
            return Ok();
        }
        
        [HttpPost("UpdateRunIntervalType")]
        public async Task<IActionResult> UpdateRunIntervalType([FromBody] CommandConfigurationDto commandConfigurationDto)
        {
            await _commandConfigurationFactory.UpdateRunIntervalType(commandConfigurationDto.Name, commandConfigurationDto.RunInterval);
            
            return Ok();
        }
    }
}