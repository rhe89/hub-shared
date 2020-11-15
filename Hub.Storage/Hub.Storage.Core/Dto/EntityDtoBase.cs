using System;

namespace Hub.Storage.Core.Dto
{
    public class EntityDtoBase
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}