using System;

namespace Hub.Storage.Repository.Dto
{
    public class EntityDtoBase
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}