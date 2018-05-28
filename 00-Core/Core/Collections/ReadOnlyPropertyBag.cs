using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AMKsGear.Core.Collections
{
    public class ReadOnlyPropertyBag : ReadOnlyPropertyBag<string, object>
    {
        public ReadOnlyPropertyBag() { }
        public ReadOnlyPropertyBag(IEnumerable<KeyValuePair<string, object>> items) : base(items) { }
    }

    public class ReadOnlyPropertyBag<TValue> : ReadOnlyPropertyBag<string, TValue>
    {
        public ReadOnlyPropertyBag() { }
        public ReadOnlyPropertyBag(IEnumerable<KeyValuePair<string, TValue>> items) : base(items) { }
    }

    public class ReadOnlyPropertyBag<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
    {
        private readonly Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        public ReadOnlyPropertyBag() { }
        public ReadOnlyPropertyBag(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (var item in items)
                _values.Add(item.Key, item.Value);
        }

        void ReadOnlyAccess() { throw new InvalidOperationException("Unable to modify a readonly collection."); }

        void IDictionary.Clear() { ReadOnlyAccess(); }
        IDictionaryEnumerator IDictionary.GetEnumerator() { return _values.GetEnumerator(); }
        public void Remove(TKey key) { ReadOnlyAccess(); }
        public void Remove(object key) { ReadOnlyAccess(); }

        object IDictionary.this[object key] { get { return this[(TKey)key]; } set { ReadOnlyAccess(); } }
        public TValue this[TKey key]
        {
            get
            {
                TValue retVal;
                _values.TryGetValue(key, out retVal);
                return retVal;
            }
            set { ReadOnlyAccess(); }
        }

        public ICollection Keys => _values.Keys;
        public ICollection Values => _values.Values;
        bool IDictionary.IsReadOnly => false;
        public bool IsFixedSize => false;

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return _values.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return _values.GetEnumerator(); }
        public void Add(KeyValuePair<TKey, TValue> item) { ReadOnlyAccess(); }
        public void Add(object key, object value) { ReadOnlyAccess(); }
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            ReadOnlyAccess();
        }
        public bool Contains(object key) { return _values.ContainsKey((TKey)key); }
        public bool Contains(TKey key) { return _values.ContainsKey(key); }
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _values.ContainsKey(item.Key) && _values[item.Key].Equals(item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear() { ReadOnlyAccess(); }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { (_values as ICollection).CopyTo(array, arrayIndex); }
        public void CopyTo(Array array, int index) { (_values as ICollection).CopyTo(array, index); }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            ReadOnlyAccess();
            return default(bool);
        }

        int ICollection.Count => _values.Count;
        public object SyncRoot => _values;
        public bool IsSynchronized => (_values as ICollection).IsSynchronized;
        int ICollection<KeyValuePair<TKey, TValue>>.Count => _values.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => (_values as ICollection<KeyValuePair<TKey, TValue>>).IsReadOnly;
        //public void GetObjectData(SerializationInfo info, StreamingContext context) { _values.GetObjectData(info, context); }
        //public void OnDeserialization(object sender) { _values.OnDeserialization(sender); }
    }
}
