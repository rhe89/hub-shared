using System;

namespace Hub.Storage.Repository
{
    public interface IScopedDbRepository : IDisposable, IDbRepository
    {
    }
}