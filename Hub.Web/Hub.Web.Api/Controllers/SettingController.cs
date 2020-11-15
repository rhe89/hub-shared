using System.Threading.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        protected readonly ISettingProvider SettingProvider;
        protected readonly ISettingFactory SettingFactory;

        public SettingController(ISettingProvider settingProvider, ISettingFactory settingFactory)
        {
            SettingProvider = settingProvider;
            SettingFactory = settingFactory;
        }
        
        [HttpGet("settings")]
        public async Task<IActionResult> Settings()
        {
            var settings = await SettingProvider.GetSettings();
            
            return Ok(settings);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateSetting([FromBody] SettingDto settingDto)
        {
            await SettingFactory.UpdateSetting(settingDto.Key, settingDto.Value);
            
            return Ok();
        }
    }
}