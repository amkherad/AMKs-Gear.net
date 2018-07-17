using System;

namespace AMKsGear.Architecture.Automation.Mapper
{
    public interface IMapperValueProvider
    {
        ValueResolverValueNotFoundBehavior ValueNotFoundBehavior { get; }
        object GetValue(string propName, Type valueType);
        object GetValue(string propName);
    }
    public interface IMapperValueProvider<out TValue> : IMapperValueProvider
    {
        new TValue GetValue(string propName);
    }
    public enum ValueResolverValueNotFoundBehavior
    {
        UseDefault,
        ThrowException
    }
}