using System;

namespace AMKsGear.Architecture.Automation.IoC.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class ResolvePropertiesAttribute : TypeResolverAttribute
    {

    }
}