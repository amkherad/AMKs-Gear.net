using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.Automation.IoC
{
    public class DynamicTypeResolverContainer : TypeResolverContainer, IDynamicTypeResolver
    {
        public object Resolve(Type fromType, Type toType, object context, IEnumerable<object> args)
        {
            if (!Mappings.TypeExists(fromType))
                RegisterType(fromType, toType, args);
            return Resolve(toType, context, args);
        }
    }
}