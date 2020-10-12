using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.ApiControllers
{
    public class BackgroundTaskConfigurationControllerBase : ControllerBase
    {
        protected readonly IBackgroundTaskConfigurationProvider BackgroundTaskConfigurationProvider;
        protected readonly IBackgroundTaskConfigurationFactory BackgroundTaskConfigurationFactory;

        public BackgroundTaskConfigurationControllerBase(IBackgroundTaskConfigurationProvider backgroundTaskConfigurationProvider,
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