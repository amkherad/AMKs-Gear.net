using System;

namespace AMKsGear.Architecture.Automation.IoC.Annotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ResolveValueAttribute : TypeResolverAttribute
    {

    }
}