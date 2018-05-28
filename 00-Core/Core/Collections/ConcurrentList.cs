using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class ConcurrentList<T> : List<T>
    {
        //TODO: Implement IList<T> as concurrent collection.
        public ConcurrentList() { }
        public ConcurrentList(IEnumerable<T> collection) : base(collection) { }
    }
}