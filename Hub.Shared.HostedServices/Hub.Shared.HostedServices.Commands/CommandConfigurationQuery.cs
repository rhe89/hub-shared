using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.HostedServices.Commands;

[DataContract]
public class CommandConfigurationQuery : Query
{
    public string Name { get; set; }

    
}