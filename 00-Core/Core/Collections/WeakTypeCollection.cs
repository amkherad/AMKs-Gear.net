using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class WeakTypeCollection : ICollection
    {
        private readonly List<object> _items = new List<object>();

        public IEnumerator GetEnumerator() => _items.GetEnumerator();

        public void CopyTo(Array array, int index) => ((ICollection) _items).CopyTo(array, index);

        public int Count => _items.Count;
        public bool IsSynchronized => ((ICollection) _items).IsSynchronized;
        public object SyncRoot => ((ICollection) _items).SyncRoot;

        public object this[int key]
        {
            get { return _items[key]; }
            set { _items[key] = value; }
        }

        public void Add(object item) => _items.Add(item);
    }
}