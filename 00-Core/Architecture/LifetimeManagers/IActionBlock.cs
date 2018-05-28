using System;

namespace AMKsGear.Architecture.LifetimeManagers
{
    public interface IActionBlock : IDisposable
    {
        void Cancel();
    }
}