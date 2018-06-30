using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IActionContext : IWrapper
    {
        IActionDescriptor ActionDescriptor { get; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
    }
}