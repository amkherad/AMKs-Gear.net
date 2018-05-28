using System;

namespace AMKsGear.Architecture.Automation
{
    public interface IMapper : IDisposable
    {
        object SourceToDestination(Type destType, object destination, Type srcType, object source);
        object SourceToDestination(Type destType, object destination, object[] sources);
        object SourceToDestination(Type destType, object destination, IValueResolver resolver);
    }
}