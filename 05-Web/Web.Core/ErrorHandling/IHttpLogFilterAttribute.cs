using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Trace;

namespace AMKsGear.Web.Core.ErrorHandling
{
    public interface IHttpLogFilterAttribute
    {
        LocalLogger Logger { get; }
        string LogCategory { get; }

        LogLevel LogLevel { get; }

    }
}