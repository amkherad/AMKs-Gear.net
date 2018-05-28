using System.Collections.Generic;

namespace AMKsGear.Architecture.Trace
{
    public interface ILoggingContext
    {
        string PlatformName { get; }
        string AppDomain { get; }
        string Application { get; }
        IDictionary<string, object> Options { get; }
    }
}