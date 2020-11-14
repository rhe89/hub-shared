using System;

namespace Hub.Storage.Core.Repository
{
    public interface IScopedHubDbRepository : IDisposable, IHubDbRepository
    {
    }
}