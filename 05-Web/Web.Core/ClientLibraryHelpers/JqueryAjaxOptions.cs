using AMKsGear.Architecture.Data.Annotations;
using AMKsGear.Web.Core.Helpers;

namespace AMKsGear.Web.Core.ClientLibraryHelpers
{
    public class JqueryAjaxOptions
    {
        public string Url { get; set; }
        public string Type { get; set; }

        public string Accepts { get; set; }
        public bool? Async { get; set; }
        public object BeforeSend { get; set; }
        public bool? Cache { get; set; }
        public object Complete { get; set; }
        public object Contents { get; set; }
        public string ContentType { get; set; }
        public object Context { get; set; }
        public string Converters { get; set; }
        public bool? CrossDomain { get; set; }
        //[RawData]
        public object Data { get; set; }
        public object DataFilter { get; set; }
        public string DataType { get; set; }
        public object Error { get; set; }
        public bool? Global { get; set; }
        public object Headers { get; set; }
        public bool? IfModified { get; set; }
        public bool? IsLocal { get; set; }
        public string Jsonp { get; set; }
        public object JsonpCallback { get; set; }
        public string Method { get; set; }
        public string MimeType { get; set; }
        public string Password { get; set; }
        public bool? ProcessData { get; set; }
        public string ScriptCharset { get; set; }
        [RawData]
        public object StatusCode { get; set; }
        [RawData]
        public object Success { get; set; }
        public decimal? Timeout { get; set; }
        public bool? Traditional { get; set; }
        public string Username { get; set; }
        public object Xhr { get; set; }
        public object XhrFields { get; set; }
        
        public override string ToString() => JsOptions.Serialize(this);
    }
}