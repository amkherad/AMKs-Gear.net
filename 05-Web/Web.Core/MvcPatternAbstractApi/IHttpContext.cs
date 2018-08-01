using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IHttpContext : IAdapter
    {
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
    }
}