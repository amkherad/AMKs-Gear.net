using System;

namespace AMKsGear.Architecture.Patterns
{
    public interface IConfigurator : IAdapter, IDisposable
    {
        bool Validate();
    }
}