using System.Collections.Generic;
using System.Threading.Tasks;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Collections;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IHttpRequest : IAdapter
    {
        string GetRawBody();
        Task<string> GetRawBodyAsync();

        NameStringCollection Form { get; }
        NameStringCollection QueryString { get; }
        NameStringCollection ServerVariables { get; }
        NameStringCollection AllParams { get; }
        NameStringCollection Headers { get; }

        IEnumerable<string> AcceptTypes { get; }
        string HttpMethod { get; }
        string ContentType { get; }
        string RawUrl { get; }
        int ContentLength { get; }
    }
}