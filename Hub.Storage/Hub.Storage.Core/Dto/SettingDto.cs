using AutoMapper;
using Hub.Storage.Core.Entities;

namespace Hub.Storage.Core.Dto
{
    [AutoMap(typeof(Setting))]
    public class SettingDto : EntityDtoBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}