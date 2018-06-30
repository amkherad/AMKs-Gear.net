using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IHttpContext : IWrapper
    {
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
    }
}