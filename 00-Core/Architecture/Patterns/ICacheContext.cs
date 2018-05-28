using System;

namespace AMKsGear.Architecture.Patterns
{
    public interface ICacheContext : IDisposable
    {
        void Clear();
    }
}