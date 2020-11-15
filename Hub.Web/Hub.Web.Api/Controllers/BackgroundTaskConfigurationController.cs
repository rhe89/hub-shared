using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackgroundTaskConfigurationController : ControllerBase
    {
        protected readonly IBackgroundTaskConfigurationProvider BackgroundTaskConfigurationProvider;
        protected readonly IBackgroundTaskConfigurationFactory BackgroundTaskConfigurationFactory;

        public BackgroundTaskConfigurationController(IBackgroundTaskConfigurationProvider backgroundTaskConfigurationProvider,
            IBackgroundTaskConfigurationFactory backgroundTaskConfigurationFactory)
        {
            BackgroundTaskConfigurationProvider = backgroundTaskConfigurationProvider;
            BackgroundTaskConfigurationFactory = backgroundTaskConfigurationFactory;
        }
        
        [HttpGet("BackgroundTaskConfigurations")]
        public async Task<IActionResult> BackgroundTaskConfigurations()
        {
            var backgroundTaskConfigurations = await BackgroundTaskConfigurationProvider.GetBackgroundTaskConfigurations();
            
            return Ok(backgroundTaskConfigurations);
        }
        
        [HttpPost("UpdateRunIntervalType")]
        public async Task<IActionResult> UpdateRunIntervalType([FromBody] BackgroundTaskConfigurationDto backgroundTaskConfigurationDto)
        {
            await BackgroundTaskConfigurationFactory.UpdateRunIntervalType(backgroundTaskConfigurationDto.Name, backgroundTaskConfigurationDto.RunIntervalType);
            
            return Ok();
        }
    }
}