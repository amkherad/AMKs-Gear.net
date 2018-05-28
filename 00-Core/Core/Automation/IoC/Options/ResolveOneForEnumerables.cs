using System;
using System.Linq;
using AMKsGear.Architecture;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Automation.IoC.Options
{
    public class ResolveOneForEnumerables : TypeResolverOption
    {
        public bool AllEnumerables { get; }
        public Type[] Types { get; }

        public ResolveOneForEnumerables()
        {
            AllEnumerables = true;
        }
        public ResolveOneForEnumerables(Type type, params Type[] types)
        {
            AllEnumerables = false;
            Types = types.Merge(type, RelativeOrder.Before).ToArray();
        }
    }
}