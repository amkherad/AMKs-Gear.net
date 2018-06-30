using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Collections;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IHttpResponse : IWrapper
    {

        NameStringCollection Headers { get; }
    }
}