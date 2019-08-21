using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using AMKsGear.Core.Collections;

namespace AMKsGear.Web.Core.Http
{
    public class HeaderCollection : NameStringsCollection
    {
        public const string Rfc7231DateFormat = "ddd, dd MMM yyyy HH:mm:ss 'GMT'"; //Same As RFC1123
        public const string Rfc7231DateFormatToString = "R"; //R = RFC1123

        #region Http Header Names

        public const string AcceptHeaderName = "Accept";
        public const string AcceptCharsetHeaderName = "Accept-Charset";
        public const string AcceptEncodingHeaderName = "Accept-Encoding";
        public const string AcceptLanguageHeaderName = "Accept-Language";
        public const string AcceptDateTimeHeaderName = "Accept-Datetime";
        public const string AcceptControlRequestMethodHeaderName = "Accept-Control-Request-Method";
        public const string AcceptControlRequestHeadersHeaderName = "Accept-Control-Request-Headers";
        public const string AcceptRangesHeaderName = "Accept-Ranges";

        public const string AuthorizationHeaderName = "Authorization";

        public const string CacheControlHeaderName = "Cache-Control";

        public const string ConnectionHeaderName = "Connection";

        public const string CookieHeaderName = "Cookie";

        public const string ContentLengthHeaderName = "Content-Length";
        public const string ContentMd5HeaderName = "Content-MD5";
        public const string ContentRangeHeaderName = "Content-Range";
        public const string ContentTypeHeaderName = "Content-Type";

        public const string DateHeaderName = "Date";

        public const string ExpectHeaderName = "Expect";

        public const string ForwardedHeaderName = "Forwarded";

        public const string FromHeaderName = "From";

        public const string HostHeaderName = "Host";

        public const string IfMatchHeaderName = "If-Match";
        public const string IfModifiedSinceHeaderName = "If-Modified-Since";
        public const string IfNoneMatchHeaderName = "If-None-Match";
        public const string IfRangeHeaderName = "If-Range";
        public const string IfUnmodifiedSinceHeaderName = "If-Unmodified-Since";

        public const string MaxForwardsHeaderName = "Max-Forwards";

        public const string OriginHeaderName = "Origin";

        public const string PragmaHeaderName = "Pragma";

        public const string ProxyAuthorizationHeaderName = "Proxy-Authorization";

        public const string RangeHeaderName = "Range";

        public const string RefererHeaderName = "Referer";

        public const string TeHeaderName = "TE";

        public const string UserAgentHeaderName = "User-Agent";

        public const string UpgradeHeaderName = "Upgrade";

        public const string ViaHeaderName = "Via";

        public const string WarningHeaderName = "Warning";

        #endregion

        public HeaderCollection()
        {
        }

        #region Helper Methods
        
        protected void SetStringOrRemoveOnNull(string key, string value)
        {
            if (value == null)
            {
                RemoveKey(key);
            }
            else
            {
                Set(key, value);
            }
        }

        protected DateTime? GetDateTimeAsRfc7231(string key)
        {
            DateTime result;
            var dateStr = SingleOrDefault(key);
            return DateTime.TryParseExact(dateStr, Rfc7231DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result)
                ? (DateTime?)result
                : null;
        }

        protected void SetDateTimeAsRfc7231(string key, DateTime? value)
        {
            if (value == null)
            {
                RemoveKey(key);
            }
            else
            {
                Set(DateHeaderName, value?.ToUniversalTime().ToString(Rfc7231DateFormatToString));
            }
        }

        protected void SetDateTimeAsRfc7231OrRemoveOnNull(string key, DateTime? value)
        {
            if (value == null)
            {
                RemoveKey(key);
            }
            else
            {
                Set(DateHeaderName, value.Value.ToUniversalTime().ToString(Rfc7231DateFormatToString));
            }
        }

        protected long? GetLong(string key)
        {
            long result;
            var longStr = SingleOrDefault(key);
            return longStr == null || !long.TryParse(longStr, out result)
                ? null
                : (long?)result;
        }
        
        protected void SetLong(string key, long? value)
        {
            if (value == null)
            {
                RemoveKey(key);
            }
            else
            {
                Set(DateHeaderName, value.ToString());
            }
        }

        protected int? GetInt(string key)
        {
            int result;
            var intStr = SingleOrDefault(key);
            return intStr == null || !int.TryParse(intStr, out result)
                ? null
                : (int?)result;
        }
        
        protected void SetInt(string key, int? value)
        {
            if (value == null)
            {
                RemoveKey(key);
            }
            else
            {
                Set(DateHeaderName, value.ToString());
            }
        }
        #endregion
        


        #region Properties
        public string Accept
        {
            get { return SingleOrDefault(AcceptHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptHeaderName, value); }
        }

        public string AcceptCharset
        {
            get { return SingleOrDefault(AcceptCharsetHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptCharsetHeaderName, value); }
        }

        public string AcceptEncoding
        {
            get { return SingleOrDefault(AcceptEncodingHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptEncodingHeaderName, value); }
        }

        public string AcceptLanguage
        {
            get { return SingleOrDefault(AcceptLanguageHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptLanguageHeaderName, value); }
        }

        public DateTime? AcceptDateTime
        {
            get { return GetDateTimeAsRfc7231(AcceptDateTimeHeaderName); }
            set { SetDateTimeAsRfc7231OrRemoveOnNull(AcceptDateTimeHeaderName, value); }
        }

        public string AcceptControlRequestMethod
        {
            get { return SingleOrDefault(AcceptControlRequestMethodHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptControlRequestMethodHeaderName, value); }
        }

        public string AcceptControlRequestHeaders
        {
            get { return SingleOrDefault(AcceptControlRequestHeadersHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptControlRequestHeadersHeaderName, value); }
        }

        public string AcceptRanges
        {
            get { return SingleOrDefault(AcceptRangesHeaderName); }
            set { SetStringOrRemoveOnNull(AcceptRangesHeaderName, value); }
        }

        public string Authorization
        {
            get { return SingleOrDefault(AuthorizationHeaderName); }
            set { SetStringOrRemoveOnNull(AuthorizationHeaderName, value); }
        }
        
        public string CacheControl
        {
            get { return SingleOrDefault(CacheControlHeaderName); }
            set { SetStringOrRemoveOnNull(CacheControlHeaderName, value); }
        }
        
        public string Connection
        {
            get { return SingleOrDefault(ConnectionHeaderName); }
            set { SetStringOrRemoveOnNull(ConnectionHeaderName, value); }
        }
        
        public IEnumerable<string> Cookies
        {
            get { return this[CookieHeaderName]; }
            set { this[CookieHeaderName] = value; }
        }

        public long? ContentLength
        {
            get { return GetLong(ContentLengthHeaderName); }
            set { SetLong(ContentLengthHeaderName, value); }
        }

        public string ContentMd5
        {
            get { return SingleOrDefault(ContentMd5HeaderName); }
            set { SetStringOrRemoveOnNull(ContentMd5HeaderName, value); }
        }

        public string ContentRange
        {
            get { return SingleOrDefault(ContentRangeHeaderName); }
            set { SetStringOrRemoveOnNull(ContentRangeHeaderName, value); }
        }

        public string ContentType
        {
            get { return SingleOrDefault(ContentTypeHeaderName); }
            set { SetStringOrRemoveOnNull(ContentTypeHeaderName, value); }
        }

        public DateTime? Date
        {
            get { return GetDateTimeAsRfc7231(DateHeaderName); }
            set { SetDateTimeAsRfc7231OrRemoveOnNull(DateHeaderName, value); }
        }

        public string Expect
        {
            get { return SingleOrDefault(ExpectHeaderName); }
            set { SetStringOrRemoveOnNull(ExpectHeaderName, value); }
        }

        public string Forwarded
        {
            get { return SingleOrDefault(ForwardedHeaderName); }
            set { SetStringOrRemoveOnNull(ForwardedHeaderName, value); }
        }

        public string From
        {
            get { return SingleOrDefault(FromHeaderName); }
            set { SetStringOrRemoveOnNull(FromHeaderName, value); }
        }

        public string Host
        {
            get { return SingleOrDefault(HostHeaderName); }
            set { SetStringOrRemoveOnNull(HostHeaderName, value); }
        }

        public string IfMatch
        {
            get { return SingleOrDefault(IfMatchHeaderName); }
            set { SetStringOrRemoveOnNull(IfMatchHeaderName, value); }
        }

        public string IfModifiedSince
        {
            get { return SingleOrDefault(IfModifiedSinceHeaderName); }
            set { SetStringOrRemoveOnNull(IfModifiedSinceHeaderName, value); }
        }

        public string IfNoneMatch
        {
            get { return SingleOrDefault(IfNoneMatchHeaderName); }
            set { SetStringOrRemoveOnNull(IfNoneMatchHeaderName, value); }
        }

        public string IfRange
        {
            get { return SingleOrDefault(IfRangeHeaderName); }
            set { SetStringOrRemoveOnNull(IfRangeHeaderName, value); }
        }

        public string IfUnmodifiedSince
        {
            get { return SingleOrDefault(IfUnmodifiedSinceHeaderName); }
            set { SetStringOrRemoveOnNull(IfUnmodifiedSinceHeaderName, value); }
        }

        public string MaxForwards
        {
            get { return SingleOrDefault(MaxForwardsHeaderName); }
            set { SetStringOrRemoveOnNull(MaxForwardsHeaderName, value); }
        }

        public string Origin
        {
            get { return SingleOrDefault(OriginHeaderName); }
            set { SetStringOrRemoveOnNull(OriginHeaderName, value); }
        }

        public string Pragma
        {
            get { return SingleOrDefault(PragmaHeaderName); }
            set { SetStringOrRemoveOnNull(PragmaHeaderName, value); }
        }

        public string ProxyAuthorization
        {
            get { return SingleOrDefault(ProxyAuthorizationHeaderName); }
            set { SetStringOrRemoveOnNull(ProxyAuthorizationHeaderName, value); }
        }

        public string  Range
        {
            get { return SingleOrDefault(RangeHeaderName); }
            set { SetStringOrRemoveOnNull(RangeHeaderName, value); }
        }

        public string  Referer
        {
            get { return SingleOrDefault(RefererHeaderName); }
            set { SetStringOrRemoveOnNull(RefererHeaderName, value); }
        }

        public string  TE
        {
            get { return SingleOrDefault(TeHeaderName); }
            set { SetStringOrRemoveOnNull(TeHeaderName, value); }
        }

        public string  UserAgent
        {
            get { return SingleOrDefault(UserAgentHeaderName); }
            set { SetStringOrRemoveOnNull(UserAgentHeaderName, value); }
        }

        public string  Upgrade
        {
            get { return SingleOrDefault(UpgradeHeaderName); }
            set { SetStringOrRemoveOnNull(UpgradeHeaderName, value); }
        }

        public string  Via
        {
            get { return SingleOrDefault(ViaHeaderName); }
            set { SetStringOrRemoveOnNull(ViaHeaderName, value); }
        }

        public string  Warning
        {
            get { return SingleOrDefault(WarningHeaderName); }
            set { SetStringOrRemoveOnNull(WarningHeaderName, value); }
        }
        #endregion
    }
}