using Hub.Storage.Repository.Dto;

namespace Hub.Settings.Core
{
    public class SettingDto : EntityDtoBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}