using Hub.Storage.Core;

namespace Hub.Storage.Repository.Entities
{
    public class Setting : EntityBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}