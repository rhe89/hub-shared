using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.Settings
{
    public class SettingDto : EntityDtoBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}