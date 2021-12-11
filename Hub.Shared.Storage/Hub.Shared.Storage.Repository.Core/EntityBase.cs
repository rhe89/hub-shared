using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Hub.Shared.Storage.Repository.Core;

[DataContract]
public abstract class EntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember]
    public long Id { get; set; }

    [DataMember]
    public DateTime CreatedDate { get; set; }
        
    [DataMember]
    public DateTime UpdatedDate { get; set; }
}