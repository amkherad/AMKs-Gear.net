using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class WeakTypeDictionary : IDictionary
    {
        private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
        
        public void Add(object key, object value) => _items.Add(key, value);
        public void Clear() => _items.Clear();
        public bool Contains(object key) => _items.ContainsKey(key);
        public IDictionaryEnumerator GetEnumerator() => _items.GetEnumerator();
        public void Remove(object key) => _items.Remove(key);
        public bool IsFixedSize => ((IDictionary) _items).IsFixedSize;
        public bool IsReadOnly => ((IDictionary)_items).IsReadOnly;
        public object this[object key]
        {
            get { return _items[key]; }
            set { _items[key] = value; }
        }
        public ICollection Keys => _items.Keys;
        public ICollection Values => _items.Values;
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
        public void CopyTo(Array array, int index) => ((IDictionary) _items).CopyTo(array, index);
        public int Count => _items.Count;
        public bool IsSynchronized => ((IDictionary) _items).IsSynchronized;
        public object SyncRoot => ((IDictionary) _items).SyncRoot;
    }
}