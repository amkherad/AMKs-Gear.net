﻿using System;
using System.Collections.Generic;

namespace AMKsGear.Architecture.Automation.IoC
{
    public interface IDynamicTypeResolver : ITypeResolver
    {
        object Resolve(Type fromType, Type toType, object context, IEnumerable<object> args);
    }
}