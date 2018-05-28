using System;

namespace AMKsGear.Architecture.LifetimeManagers
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}