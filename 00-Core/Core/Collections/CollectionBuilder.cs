using System.Collections;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Collections
{
    public class CollectionBuilder<TElement> : ICollection<TElement>, IAdapter
    {
        protected readonly List<TElement> InnerList = new List<TElement>();

        public CollectionBuilder()
        {
            
        }
        
        public CollectionBuilder<TElement> Add(TElement item)
        {
            InnerList.Add(item);
            return this;
        }
        public CollectionBuilder<TElement> AddRange(IEnumerable<TElement> collection)
        {
            InnerList.AddRange(collection);
            return this;
        }
        public CollectionBuilder<TElement> Clear()
        {
            InnerList.Clear();
            return this;
        }
        public CollectionBuilder<TElement> CopyTo(TElement[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
            return this;
        }

        #region ICollection Members
        public IEnumerator<TElement> GetEnumerator() => InnerList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InnerList.GetEnumerator();
        void ICollection<TElement>.Add(TElement item) => InnerList.Add(item);
        void ICollection<TElement>.Clear() => InnerList.Clear();
        public bool Contains(TElement item) => InnerList.Contains(item);
        void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex) => InnerList.CopyTo(array, arrayIndex);
        public bool Remove(TElement item) => InnerList.Remove(item);
        public int Count => InnerList.Count;
        public bool IsReadOnly => ((ICollection<TElement>) InnerList).IsReadOnly;
        #endregion

        public object GetUnderlyingContext() => InnerList;
    }
}