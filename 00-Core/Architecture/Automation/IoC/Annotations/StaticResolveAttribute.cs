using System;

namespace AMKsGear.Architecture.Automation.IoC.Annotations
{
    public class StaticResolveAttribute : TypeResolverAttribute
    {
        public Type StaticType { get; }

        public StaticResolveAttribute(Type staticType)
        {
            StaticType = staticType;
        }
    }
}