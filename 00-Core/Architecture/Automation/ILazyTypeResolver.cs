using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Architecture.Automation
{
    public interface ILazyTypeResolver : ITypeResolver
    {
        object Resolve(Type fromType, Type toType, object context, IEnumerable<object> args);
    }
}