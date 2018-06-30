using System;
using System.Globalization;

namespace AMKsGear.Web.Core.Http
{
    public class HttpCookie
    {
        public virtual string Name { get; }
        public virtual string Value { get; set; }
        public virtual string Path { get; set; }
        public virtual string Domain { get; set; }
        public virtual DateTime? Expires { get; set; }
        public virtual Uri Uri { get; set; }
        public virtual bool Secure { get; set; }
        public virtual bool HttpOnly { get; set; }
        //public string
        
        
        protected HttpCookie() { }
        public HttpCookie(string name)
        {
            Name = name;
        }

        public virtual string[] Values
        {
            get { return Value.Split(';'); }
            set { Value = string.Join(";", value); }
        }

        public virtual void FillFromString(string cookieString)
        {
            if (cookieString == null) throw new ArgumentNullException(nameof(cookieString));

            const string SetCookie = "set-cookie:";
            
            cookieString = cookieString.Trim();
            var lower = cookieString.ToLower();
            if (lower.StartsWith(SetCookie))
            {
                cookieString = cookieString.Substring(SetCookie.Length);
            }

            Value = cookieString;

            var parts = cookieString.Split(';');
            foreach (var part in parts)
            {
                var eqIndex = part.IndexOf('=');
                if (eqIndex == -1)
                {
                    switch (part.ToLower())
                    {
                        case "secure":
                        {
                            Secure = true;
                            break;
                        }
                        case "httponly":
                        {
                            HttpOnly = true;
                            break;
                        }
                    }
                }
                else
                {
                    var prop = part.Substring(0, eqIndex);
                    var value = part.Substring(eqIndex + 1);
                    switch (prop.ToLower())
                    {
                        case "expires":
                        case "expire":
                        {
                            DateTime expiration;
                            if (DateTime.TryParseExact(value, HeaderCollection.Rfc7231DateFormat,
                                CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out expiration))
                            {
                                Expires = expiration;
                            }
                            break;
                        }
                        case "path":
                        {
                            Path = value;
                            break;
                        }
                        case "domain":
                        {
                            Domain = value;
                            break;
                        }
                        case "__host-id":
                        {
                            Domain = value;
                            break;
                        }
                        case "secure":
                        {
                            value = value.ToLower();
                            Secure = value == "secure" || value == "true" || value == "1";
                            break;
                        }
                        case "httponly":
                        {
                            value = value.ToLower();
                            HttpOnly = value == "httponly" || value == "true" || value == "1" || value == "http-only";
                            break;
                        }
                    }
                }
            }
        }

        public static HttpCookie Parse(string cookieString)
        {
            var cookie = new HttpCookie();
            cookie.FillFromString(cookieString);
            return cookie;
        }
    }
}