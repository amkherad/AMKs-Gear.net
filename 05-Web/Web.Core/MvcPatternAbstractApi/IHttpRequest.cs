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

        NameStringsCollection Form { get; }
        NameStringsCollection QueryString { get; }
        NameStringsCollection ServerVariables { get; }
        NameStringsCollection AllParams { get; }
        NameStringsCollection Headers { get; }

        IEnumerable<string> AcceptTypes { get; }
        string HttpMethod { get; }
        string ContentType { get; }
        string RawUrl { get; }
        int ContentLength { get; }
    }
}