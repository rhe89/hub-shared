using System;

namespace Hub.Shared.HostedServices.Core
{
    public class HostedServicesException : Exception
    {
        public HostedServicesException(string message) : base(message)
        {
        }
    }
}