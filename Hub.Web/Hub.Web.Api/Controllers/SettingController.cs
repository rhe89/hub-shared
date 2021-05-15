using System.Threading.Tasks;
using Hub.Settings.Core;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingProvider _settingProvider;
        private readonly ISettingFactory _settingFactory;

        public SettingController(ISettingProvider settingProvider, ISettingFactory settingFactory)
        {
            _settingProvider = settingProvider;
            _settingFactory = settingFactory;
        }
        
        [HttpGet("settings")]
        public async Task<IActionResult> Settings()
        {
            var settings = await _settingProvider.GetSettings();
            
            return Ok(settings);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateSetting([FromBody] SettingDto settingDto)
        {
            await _settingFactory.UpdateSetting(settingDto.Key, settingDto.Value);
            
            return Ok();
        }
    }
}