using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Web.Core.Http
{
    public class HeaderCookieCollection : CookieCollection// ICollection<HttpCookie>
    {
        public HeaderCollection Headers { get; }
        
        
        public HeaderCookieCollection(HeaderCollection headers)
        {
            Headers = headers;
        }
        
        
        
        
        
        #region ICollection implementations
        
        public int Count => Headers.CountEntries(HeaderCollection.CookieHeaderName);

        //public IEnumerator<HttpCookie> GetEnumerator() => Headers.GetEnumerator();

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public void Add(HttpCookie item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(HttpCookie item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(HttpCookie[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(HttpCookie item)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly { get; }
        
        #endregion
    }
}