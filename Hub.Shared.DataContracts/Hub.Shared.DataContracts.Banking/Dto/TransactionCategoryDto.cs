using System.Collections.Generic;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

public class TransactionCategoryDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }
    
    [DataMember]
    public IList<TransactionSubCategoryDto> TransactionSubCategories { get; set; }
}