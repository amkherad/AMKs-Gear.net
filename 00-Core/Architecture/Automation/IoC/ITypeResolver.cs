using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Automation.IoC
{
    public interface ITypeResolver : IAdapter
    {
        object Resolve(Type type, object context, IEnumerable<object> args);
        bool CanResolve(Type type, object context, IEnumerable<object> args);
    }
}