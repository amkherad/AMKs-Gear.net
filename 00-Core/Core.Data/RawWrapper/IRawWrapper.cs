using System;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data.RawWrapper
{
    public interface IRawWrapper : IAdapter, IDisposable
    {
        IRawWrapperMetaData Meta { get; }

        //ITransaction BeginTransaction();
    }
}