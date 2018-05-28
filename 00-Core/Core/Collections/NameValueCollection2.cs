//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//
//namespace AMKsGear.Core.Collections
//{
//    public abstract class NameValueCollectionBase<TValue> : ICollection
//    {
//        private Dictionary<string, int> _indexByKey;
//        private List<Entry> _entries;
//        private int? _nullKeyIndex;
//        private readonly int _initialCapacity;
//        private KeysCollection _keys;
//
//        [DebuggerDisplay("{ Key = {Key}, Value = {Value} }")]
//        private struct Entry
//        {
//            public readonly string Key;
//            public readonly IEnumerable<TValue> Value;
//
//            public Entry(string key, IEnumerable<TValue> value)
//            {
//                Debug.Assert(key != null);
//
//                Key = key;
//                Value = value;
//            }
//        }
//
//        internal IEqualityComparer<string> EqualityComparer { get; private set; }
//
//        protected NameValueCollectionBase() :
//            this(0)
//        { }
//
//        protected NameValueCollectionBase(int capacity) :
//            this(capacity, null)
//        { }
//
//        protected NameValueCollectionBase(IEqualityComparer<string> equalityComparer) :
//            this(0, equalityComparer)
//        { }
//
//        protected NameValueCollectionBase(int capacity, IEqualityComparer<string> equalityComparer)
//        {
//            EqualityComparer = equalityComparer ?? StringComparer.CurrentCultureIgnoreCase;
//            _initialCapacity = capacity;
//            Reset();
//        }
//
//        private void Reset()
//        {
//            _indexByKey = new Dictionary<string, int>(_initialCapacity, EqualityComparer);
//            _entries = new List<Entry>(_initialCapacity);
//            _nullKeyIndex = null;
//        }
//
//        public virtual KeysCollection Keys
//        {
//            get
//            {
//                if (_keys == null)
//                    _keys = new KeysCollection(this);
//                return _keys;
//            }
//        }
//
//        public virtual IEnumerator GetEnumerator()
//        {
//            return Keys.GetEnumerator();
//        }
//
//        public virtual int Count => _entries.Count;
//
//        bool ICollection.IsSynchronized => false;
//
//        object ICollection.SyncRoot => this;
//
//        void ICollection.CopyTo(Array array, int index)
//        {
//            ((ICollection)Keys).CopyTo(array, index);
//        }
//
//        protected bool IsReadOnly { get; set; }
//
//        protected void BaseAdd(string name, TValue value)
//        {
//            RequireWriteAccess();
//            #warning COMMENTED
//            //var entry = new Entry(name, value);
//            //var index = _entries.Count;
//            //
//            //if (name == null)
//            //{
//            //    if (_nullKeyIndex == null)
//            //        _nullKeyIndex = index;
//            //}
//            //else
//            //{
//            //    int unused;
//            //    if (!_indexByKey.TryGetValue(name, out unused))
//            //        _indexByKey.Add(name, index);
//            //}
//            //
//            //_entries.Add(entry);
//        }
//
//        protected void BaseClear()
//        {
//            RequireWriteAccess();
//            Reset();
//        }
//
//        protected IEnumerable<TValue> BaseGet(int index)
//        {
//            return _entries[index].Value;
//        }
//
//        protected IEnumerable<TValue> BaseGet(string name)
//        {
//            var index = NameToIndex(name);
//            return index == null ? default(IEnumerable<TValue>) : _entries[index.Value].Value;
//        }
//
//        protected string[] BaseGetAllKeys()
//        {
//            return _entries.Select(e => e.Key).ToArray();
//        }
//
//        protected TValue[] BaseGetAllValues()
//        {
//            return _entries.SelectMany((e, i) => BaseGet(i)).ToArray();
//        }
//
//        protected TValue[] BaseGetAllValues(Type type)
//        {
//            if (type == null)
//                throw new ArgumentNullException(nameof(type));
//
//            var count = _entries.Count;
//            var values = (TValue[])Array.CreateInstance(type, count);
//            //for (var i = 0; i < count; i++)
//            //    values[i] = BaseGet(i);
//            #warning COMMENDED FOR LATER FIX.
//            return values;
//        }
//
//        protected string BaseGetKey(int index)
//        {
//            return _entries[index].Key;
//        }
//
//        protected bool BaseHasKeys()
//        {
//            return _indexByKey.Count > 0;
//        }
//
//        protected void BaseRemove(string name)
//        {
//            RequireWriteAccess();
//
//            if (name != null)
//                _indexByKey.Remove(name);
//            else
//                _nullKeyIndex = null;
//
//            var count = _entries.Count;
//            for (var i = 0; i < count;)
//            {
//                var key = BaseGetKey(i);
//                if (EqualityComparer.Equals(key, name))
//                {
//                    _entries.RemoveAt(i);
//                    count--;
//                }
//                else
//                {
//                    i++;
//                }
//            }
//        }
//
//        protected void BaseRemoveAt(int index)
//        {
//            RequireWriteAccess();
//
//            var key = BaseGetKey(index);
//
//            if (key != null)
//                _indexByKey.Remove(key);
//            else
//                _nullKeyIndex = null;
//
//            _entries.RemoveAt(index);
//        }
//
//        protected void BaseSet(int index, TValue value)
//        {
//            RequireWriteAccess();
//            var current = _entries[index];
//            #warning COMMENDED
//            //_entries[index] = new Entry(current.Key, value);
//        }
//
//        protected void BaseSet(string name, TValue value)
//        {
//            RequireWriteAccess();
//
//            var index = NameToIndex(name);
//            #warning COMMENDED
//            //if (index != null)
//            //    _entries[index.Value] = new Entry(name, value);
//            //else
//            //    BaseAdd(name, value);
//        }
//
//        protected void RequireWriteAccess()
//        {
//            if (IsReadOnly)
//                throw new NotSupportedException("Collection is read-only.");
//        }
//
//        private int? NameToIndex(string name)
//        {
//            int index;
//            return name == null
//                 ? _nullKeyIndex
//                 : _indexByKey.TryGetValue(name, out index)
//                   ? index
//                   : (int?)null;
//        }
//
//        public class KeysCollection : ICollection
//        {
//            private readonly NameValueCollectionBase<TValue> _collection;
//
//            internal KeysCollection(NameValueCollectionBase<TValue> collection)
//            {
//                _collection = collection;
//            }
//
//            public string this[int index] => Get(index);
//            public int Count => _collection.Count;
//            bool ICollection.IsSynchronized => false;
//            object ICollection.SyncRoot => _collection;
//
//            public virtual string Get(int index)
//            {
//                return _collection.BaseGetKey(index);
//            }
//
//            public IEnumerator GetEnumerator()
//            {
//                foreach (var item in _collection._entries)
//                    yield return item.Key;
//            }
//
//            void ICollection.CopyTo(Array array, int arrayIndex)
//            {
//                _collection._entries.Select(e => e.Key).ToArray().CopyTo(array, arrayIndex);
//            }
//        }
//    }
//
//    public abstract class NameValueCollection : NameValueCollection<object> { }
//    public class NameValueCollection<TValue> : NameValueCollectionBase<TValue>
//    {
//        private string[] _cachedKeys;
//        private string[] _cachedValues;
//
//        public NameValueCollection() { }
//
//        public NameValueCollection(int capacity) :
//            base(capacity)
//        { }
//
//        public NameValueCollection(NameValueCollection collection) :
//            base(collection?.EqualityComparer)
//        {
//            if (collection == null) throw new ArgumentNullException(nameof(collection));
//            Add(collection);
//        }
//
//        public NameValueCollection(int capacity, NameValueCollection col) :
//            base(capacity, col?.EqualityComparer)
//        {
//            Add(col);
//        }
//
//        public NameValueCollection(IEqualityComparer<string> equalityComparer) :
//            base(equalityComparer)
//        { }
//
//        public NameValueCollection(int capacity, IEqualityComparer<string> equalityComparer) :
//            base(capacity, equalityComparer)
//        { }
//
//        public virtual string[] AllKeys
//        {
//            get
//            {
//                if (_cachedKeys == null)
//                    _cachedKeys = BaseGetAllKeys();
//
//                return _cachedKeys;
//            }
//        }
//
//        public TValue this[int index] => Get(index);
//
//        public TValue this[string name]
//        {
//            get { return Get(name); }
//            set { Set(name, value); }
//        }
//
//        private List<TValue> GetStringList(string name)
//        {
//            return (List<TValue>)BaseGet(name);
//        }
//
//        private List<TValue> GetStringList(int index)
//        {
//            return (List<TValue>)BaseGet(index);
//        }
//
//        public void Add(NameValueCollection collection)
//        {
//            if (collection == null) throw new ArgumentNullException(nameof(collection));
//
//            RequireWriteAccess();
//
//            InvalidateCachedArrays();
//            var count = collection.Count;
//            for (var i = 0; i < count; i++)
//            {
//                var key = collection.GetKey(i);
//                var those = (IEnumerable<TValue>)collection.BaseGet(i);
//                var these = (List<TValue>)BaseGet(key);
//                if (these != null && those != null)
//                    these.AddRange(those);
//                else if (those != null)
//                    these = new List<TValue>(those);
//                //BaseSet(key, these);
//                #warning COMMENDED
//            }
//        }
//
//        public virtual void Add(string name, TValue val)
//        {
//            RequireWriteAccess();
//
//            InvalidateCachedArrays();
//            var values = GetStringList(name);
//            if (values == null)
//            {
//                #warning COMMENDED
//                //values = new List<string>();
//                //if (val != null)
//                //    values.Add(val);
//                //BaseAdd(name, values);
//            }
//            else
//            {
//                if (val != null)
//                    values.Add(val);
//            }
//
//        }
//
//        public virtual void Clear()
//        {
//            RequireWriteAccess();
//            InvalidateCachedArrays();
//            BaseClear();
//        }
//
//        public void CopyTo(Array dest, int index)
//        {
//            if (dest == null) throw new ArgumentNullException(nameof(dest));
//
//            #warning COMMENDED
//            //if (_cachedValues == null)
//            //    _cachedValues = Enumerable.Range(0, Count).Select(i => Get(i)).ToArray();
//
//            _cachedValues.CopyTo(dest, index);
//        }
//
//        public virtual TValue Get(int index)
//        {
//            #warning COMMENDED
//            return default(TValue);
//            //return ToDelimitedString(GetStringList(index));
//        }
//
//        public virtual TValue Get(string name)
//        {
//            #warning COMMENDED
//            return default(TValue);
//            //return ToDelimitedString(GetStringList(name));
//        }
//
//        public virtual string GetKey(int index)
//        {
//            return BaseGetKey(index);
//        }
//
//        public virtual TValue[] GetValues(int index)
//        {
//            #warning COMMENDED
//            return null;
//            //return ToStringArray(GetStringList(index));
//        }
//
//        public virtual TValue[] GetValues(string name)
//        {
//            #warning COMMENDED
//            return null;
//            //return ToStringArray(GetStringList(name));
//        }
//
//        public bool HasKeys()
//        {
//            return BaseHasKeys();
//        }
//
//        public virtual void Remove(string name)
//        {
//            RequireWriteAccess();
//            InvalidateCachedArrays();
//            BaseRemove(name);
//        }
//
//        public virtual void Set(string name, TValue value)
//        {
//            RequireWriteAccess();
//            InvalidateCachedArrays();
//
//            var values = new List<TValue>();
//            if (value != null)
//            {
//                values.Add(value);
//                #warning COMMENDED
//                //BaseSet(name, values);
//            }
//            else
//            {
//                #warning COMMENDED
//                //BaseSet(name, null);
//            }
//        }
//
//        public IEnumerable<KeyValuePair<string, TValue>> ToKeyValuePairs()
//        {
//            var allKeys = AllKeys;
//
//            foreach (var key in allKeys)
//            {
//                var values = GetValues(key);
//                foreach (var value in values)
//                {
//                    yield return new KeyValuePair<string, TValue>(key, value);
//                }
//            }
//        }
//
//        protected void InvalidateCachedArrays()
//        {
//            _cachedKeys = null;
//            _cachedValues = null;
//        }
//
//        private static TValue ToDelimitedString(ICollection<string> values)
//        {
//            #warning COMMENDED
//            //if (values == null || values.Count == 0)
//            //    return null;
//
//            using (var e = values.GetEnumerator())
//            {
//                e.MoveNext();
//                var sb = new StringBuilder(e.Current);
//                while (e.MoveNext())
//                    sb.Append(',').Append(e.Current);
//                #warning COMMENDED
//                return default(TValue);
//                //return sb.ToString();
//            }
//        }
//
//        private static TValue[] ToStringArray(ICollection<string> values)
//        {
//            #warning COMMENDED
//            return null;
//            //return values == null || values.Count == 0 ? null : values.ToArray();
//        }
//    }
//}