using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.Settings
{
    public class Setting : EntityBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}