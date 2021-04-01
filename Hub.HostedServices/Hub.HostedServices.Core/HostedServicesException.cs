using System;

namespace Hub.HostedServices.Core
{
    public class HostedServicesException : Exception
    {
        public HostedServicesException(string message) : base(message)
        {
        }
    }
}