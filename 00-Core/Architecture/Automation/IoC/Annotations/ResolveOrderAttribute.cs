using System;

namespace AMKsGear.Architecture.Automation.IoC.Annotations
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ResolveOrderAttribute : TypeResolverAttribute
    {
        public int ResolveOrder { get; }

        public ResolveOrderAttribute() { }
        public ResolveOrderAttribute(int resolveOrder)
        {
            ResolveOrder = resolveOrder;
        }
    }
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ResolveOrderMaxAttribute : ResolveOrderAttribute
    {
        public ResolveOrderMaxAttribute()
            : base(int.MaxValue)
        { }
    }
}