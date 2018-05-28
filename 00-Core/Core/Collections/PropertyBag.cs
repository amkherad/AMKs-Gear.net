using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class PropertyBag : PropertyBag<string, object>
    {
        public PropertyBag() { }
        public PropertyBag(IEnumerable<KeyValuePair<string, object>> items, Func<string, object> valueCreator) : base(items, valueCreator) { }

        public new ReadOnlyPropertyBag AsReadOnly() { return new ReadOnlyPropertyBag(_values); }
    }

    public class PropertyBag<TValue> : PropertyBag<string, TValue>
    {
        public PropertyBag() { }
        public PropertyBag(IEnumerable<KeyValuePair<string, TValue>> items, Func<string, TValue> valueCreator) : base(items, valueCreator) { }

        public new ReadOnlyPropertyBag<TValue> AsReadOnly() { return new ReadOnlyPropertyBag<TValue>(_values); }
    }

    public class PropertyBag<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
    {
        protected readonly Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        public Func<TKey, TValue> ValueCreator { get; protected set; }

        public PropertyBag() { }
        public PropertyBag(Func<TKey, TValue> valueCreator)
        {
            ValueCreator = valueCreator;
        }
        public PropertyBag(IEnumerable<KeyValuePair<TKey, TValue>> items, Func<TKey, TValue> valueCreator)
        {
            foreach (var item in items)
                _values.Add(item.Key, item.Value);
            ValueCreator = valueCreator;
        }


        void IDictionary.Clear() => _values.Clear();
        IDictionaryEnumerator IDictionary.GetEnumerator() => _values.GetEnumerator();

        public bool ContainsKey(TKey key) => _values.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => _values.TryGetValue(key, out value);


        object IDictionary.this[object key] { get { return this[(TKey)key]; } set { this[(TKey) key] = (TValue)value; } }
        public TValue this[TKey key]
        {
            get
            {
                TValue retVal;
                if (!_values.TryGetValue(key, out retVal))
                {
                    var vc = ValueCreator;
                    if (vc != null) retVal = vc(key);
                }
                return retVal;
            }
            set
            {
                if (_values.ContainsKey(key))
                    _values[key] = value;
                else
                    _values.Add(key, value);
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values => _values.Values;
        ICollection<TKey> IDictionary<TKey, TValue>.Keys => _values.Keys;

        public ICollection Keys => _values.Keys;
        public ICollection Values => _values.Values;
        bool IDictionary.IsReadOnly => false;
        public bool IsFixedSize => false;

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => _values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
        public void Add(TKey key, TValue value) => _values.Add(key, value);
        public void Add(KeyValuePair<TKey, TValue> item) => _values.Add(item.Key, item.Value);
        public void Add(object key, object value) => _values.Add((TKey)key, (TValue)value);

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (var item in items)
                _values.Add(item.Key, item.Value);
        }
        public bool Contains(object key) { return _values.ContainsKey((TKey)key); }
        public bool Contains(TKey key) { return _values.ContainsKey(key); }
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _values.ContainsKey(item.Key) && _values[item.Key].Equals(item.Value);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) => _values.Remove(item.Key);
        public void Remove(TKey key) => _values.Remove(key);
        public void Remove(object key) => _values.Remove((TKey)key);
        bool IDictionary<TKey, TValue>.Remove(TKey key) => _values.Remove(key);

        public ReadOnlyPropertyBag<TKey, TValue> AsReadOnly() => new ReadOnlyPropertyBag<TKey, TValue>(_values);

        void ICollection<KeyValuePair<TKey, TValue>>.Clear() => _values.Clear();
        public void Clear() => _values.Clear();

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => (_values as ICollection).CopyTo(array, arrayIndex);
        public void CopyTo(Array array, int index) => (_values as ICollection).CopyTo(array, index);

        int ICollection.Count => _values.Count;
        public object SyncRoot => _values;
        public bool IsSynchronized => (_values as ICollection).IsSynchronized;
        int ICollection<KeyValuePair<TKey, TValue>>.Count => _values.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => (_values as ICollection<KeyValuePair<TKey, TValue>>).IsReadOnly;
        //public void GetObjectData(SerializationInfo info, StreamingContext context) { _values.GetObjectData(info, context); }
        //public void OnDeserialization(object sender) { _values.OnDeserialization(sender); }
    }
}
