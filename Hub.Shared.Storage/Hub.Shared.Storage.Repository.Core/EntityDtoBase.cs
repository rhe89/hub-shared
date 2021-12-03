using System;

namespace Hub.Shared.Storage.Repository.Core
{
    public class EntityDtoBase
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}