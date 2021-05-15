using Hub.Storage.Repository.Entities;

namespace Hub.Settings.Core
{
    public class Setting : EntityBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}