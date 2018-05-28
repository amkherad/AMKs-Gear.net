using System;

namespace AMKsGear.Architecture.Automation
{
    public interface IValueResolver
    {
        ValueResolverValueNotFoundBehavior ValueNotFoundBehavior { get; }
        object GetValue(string propName, Type valueType);
        object GetValue(string propName);
    }
    public interface IValueResolver<out TValue> : IValueResolver
    {
        new TValue GetValue(string propName);
    }
    public enum ValueResolverValueNotFoundBehavior
    {
        UseDefault,
        ThrowException
    }
}