using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.HostedServices.Commands;

[DataContract]
public class CommandConfigurationDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public DateTime LastRun { get; set; }
        
    [DataMember]
    public string RunInterval { get; set; }
}