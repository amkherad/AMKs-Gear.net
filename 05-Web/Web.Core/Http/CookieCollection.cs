using System;
using System.Collections;
using System.Collections.Generic;
using AMKsGear.Core.Collections;

namespace AMKsGear.Web.Core.Http
{
    public class CookieCollection : List<HttpCookie>, ICollection<HttpCookie>
    {
        public CookieCollection()
        {
        }
    }
}