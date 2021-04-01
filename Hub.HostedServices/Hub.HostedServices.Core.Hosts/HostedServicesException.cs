using System;

namespace Hub.HostedServices.Core.Hosts
{
    public class HostedServicesException : Exception
    {
        public HostedServicesException(string message) : base(message)
        {
        }
    }
}