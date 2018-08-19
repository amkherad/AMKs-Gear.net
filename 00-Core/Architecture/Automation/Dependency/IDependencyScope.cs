using System;

namespace AMKsGear.Architecture.Automation.Dependency
{
    public interface IDependencyScope : IDisposable
    {
        IDependencyScope Clone();
    }
}