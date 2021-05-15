using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Hub.ServiceBus.Core
{
    public interface IQueueProcessor
    {
        Task Start(string queueName, Func<ProcessMessageEventArgs, Task> messageHandler);
    }
}