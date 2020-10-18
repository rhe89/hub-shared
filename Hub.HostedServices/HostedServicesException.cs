using System;

namespace Hub.HostedServices
{
    public class HostedServicesException : Exception
    {
        public HostedServicesException(string message) : base(message)
        {
        }
    }
}