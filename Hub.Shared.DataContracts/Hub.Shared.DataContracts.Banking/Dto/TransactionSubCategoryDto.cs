using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class TransactionSubCategoryDto : DtoBase
{
    private string _keywords;

    [DataMember]
    public long TransactionCategoryId { get; set; }
    
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Keywords
    {
        get => _keywords;
        set 
        {
            _keywords = value;
            KeywordList = _keywords.Split(",", StringSplitOptions.TrimEntries).Select(keyword => new Keyword { Value = keyword}).ToList();
        }
    }

    [JsonIgnore] 
    public IList<Keyword> KeywordList { get; set; }
    
    [DataMember]
    public TransactionCategoryDto TransactionCategory { get; set; }
}

public class Keyword
{
    public string Value { get; set; }
}