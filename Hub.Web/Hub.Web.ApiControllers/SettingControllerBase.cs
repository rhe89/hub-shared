using System.Threading.Tasks;
using Hub.Storage.Dto;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Web.ApiControllers
{
    public class SettingControllerBase : ControllerBase
    {
        protected readonly ISettingProvider SettingProvider;
        protected readonly ISettingFactory SettingFactory;

        public SettingControllerBase(ISettingProvider settingProvider, ISettingFactory settingFactory)
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