using System;
using AMKsGear.Architecture.LifetimeManagers;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data.RawWrapper
{
    public interface IRawWrapper : IWrapper, IDisposable
    {
        IRawWrapperMetaData Meta { get; }

        ITransaction BeginTransaction();
    }
}