using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class CsvImportDto : DtoBase
{
    [DataMember]
    public string FileName { get; set; }
}