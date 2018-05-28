using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Trace.PerObjectTracing.Settings
{
    internal class TraceNamedGroupSettings : TraceObjectGroupSettings
    {
        public IDictionary<string, Type> IncludedTypes= new Dictionary<string, Type>();

        public override bool IsMemberOfGroup(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}