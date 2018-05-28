using System;

namespace AMKsGear.Architecture.Patterns
{
    public interface ITypeCacheContext<TInfo> : ICacheContext
    {
        bool GetState(Type type, out TInfo info);
        void Store(Type type, TInfo info);
    }
}