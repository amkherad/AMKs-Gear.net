using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AMKsGear.Core.Collections
{
    public class KeyValuesCollection<TKey, TValue> : ICollection, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, Entry> _entries;
        private List<TKey> _orderedKeys;
        private readonly IEqualityComparer<TKey> _keyComparer;

        protected class Entry : List<TValue>
        {
            public TKey Key { get; }

            public Entry(TKey key)
            {
                Key = key;
            }

            public Entry(TKey key, IEnumerable<TValue> values)
                : base(values)
            {
                Key = key;
            }
        }

        public KeyValuesCollection()
        {
            _entries = new Dictionary<TKey, Entry>();
            _orderedKeys = new List<TKey>();
            _keyComparer = EqualityComparer<TKey>.Default;
        }
        public KeyValuesCollection(IEqualityComparer<TKey> comparer)
        {
            _entries = new Dictionary<TKey, Entry>();
            _orderedKeys = new List<TKey>();
            _keyComparer = comparer;
        }

        #region ICollection implementation

        public virtual void CopyTo(Array array, int index) => ((ICollection) _entries).CopyTo(array, index);
        public virtual int Count => _entries.Count;

        public virtual bool IsSynchronized => ((ICollection) _entries).IsSynchronized;
        public virtual object SyncRoot => ((ICollection) _entries).SyncRoot;

        #endregion


        #region IEnumerable implementation

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => new KeyValuesCollectionEnumerator(this);
        public virtual IEnumerator GetEnumerator() => new KeyValuesCollectionEnumerator(this);

        private class KeyValuesCollectionEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private readonly KeyValuesCollection<TKey, TValue> _parent;
            private int _currentIndex = 0;
            private int _currentSubIndex = 0;
            //private TKey _currentKey;


            public KeyValuesCollectionEnumerator(KeyValuesCollection<TKey, TValue> collection)
            {
                _parent = collection;
            }

            #region IDisposable implementation

            public void Dispose()
            {
                //_parent = null;
                _currentIndex = 0;
                _currentSubIndex = 0;
            }

            #endregion

            #region IEnumerator implementation

            public bool MoveNext()
            {
                if (_parent.Count == 0 || _parent.Count <= _currentIndex)
                {
                    return false;
                }

                ++_currentSubIndex;

                var col = _parent.GetEntryByIndex(_currentIndex);
                while (col.Count == 0 || col.Count <= _currentSubIndex)
                {
                    _currentSubIndex = 0;
                    ++_currentIndex;
                    if (_parent.Count <= _currentIndex)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Reset()
            {
                _currentIndex = 0;
                _currentSubIndex = 0;
            }

            object IEnumerator.Current
            {
                get
                {
                    var entry = _parent.GetEntryByIndex(_currentIndex);
                    return new KeyValuePair<TKey, TValue>(entry.Key, entry[_currentSubIndex]);
                }
            }

            #endregion

            #region IEnumerator implementation
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    var entry = _parent.GetEntryByIndex(_currentIndex);
                    return new KeyValuePair<TKey, TValue>(entry.Key, entry[_currentSubIndex]);
                }
            }

            #endregion
        }

        #endregion


        #region Public Members

        public virtual bool ContainsKey(TKey key) => _entries.ContainsKey(key);
        public virtual int CountEntries(TKey key) => GetEntryByKey(key)?.Count ?? 0;

        public virtual IEnumerable<TValue> this[TKey key]
        {
            get
            {
                return GetEntryByKey(key);
            }
            set
            {
                if (_entries.ContainsKey(key))
                {
                    _entries[key] = new Entry(key, value);
                }
                else
                {
                    _entries.Add(key, new Entry(key, value));
                    _orderedKeys.Add(key);
                }
            }
        }
        public virtual IEnumerable<TValue> this[int index]
        {
            get
            {
                return GetEntryByIndex(index);
            }
            set
            {
                var key = _orderedKeys[index];
                _entries[key] = new Entry(key, value);
            }
        }
        
        public virtual IEnumerable<TValue> Get(TKey key) => GetEntryByKey(key);

        public virtual void Set(TKey key, IEnumerable<TValue> values)
        {
            if (_entries.ContainsKey(key))
            {
                _entries[key] = new Entry(key, values);
            }
            else
            {
                _entries.Add(key, new Entry(key, values));
                _orderedKeys.Add(key);
            }
        }

        public virtual void Set(TKey key, TValue value) => Set(key, new[] {value});

        public virtual void Clear()
        {
            _entries = new Dictionary<TKey, Entry>();
            _orderedKeys = new List<TKey>();
        }
        
        public virtual TValue First(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.First();
        }
        public virtual TValue FirstOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.FirstOrDefault();
        }
        public virtual TValue Single(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.Single();
        }
        public virtual TValue SingleOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.SingleOrDefault();
        }
        public virtual TValue Last(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.Last();
        }
        public virtual TValue LastOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.LastOrDefault();
        }
        
        public virtual void Add(TKey key, TValue value)
        {
            var col = GetOrNewEntryByKey(key);
            col.Add(value);
        }

        public virtual void Add(TKey key, IEnumerable<TValue> values)
        {
            var col = GetOrNewEntryByKey(key);
            col.AddRange(values);
        }

        public virtual bool Remove(TKey key, TValue value)
        {
            var col = GetEntryByKey(key);
            if (col != null)
            {
                return false;
            }
            col.Remove(value);
            return true;
        }
        public virtual void RemoveAll(TKey key, IEnumerable<TValue> values)
        {
            var col = GetEntryByKey(key);
            if (col != null)
            {
                return;
            }
            col.RemoveAll(values);
        }
        public virtual void RemoveKey(TKey key)
        {
            _entries.Remove(key);
            _orderedKeys.RemoveAll(r => _keyComparer.Equals(r, key));
        }

        public virtual IEnumerable<TKey> Keys => _entries.Keys;
        public virtual IEnumerable<TValue> Values => _entries.SelectMany(entry => entry.Value);

        public virtual IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs()
        {
            var allKeys = _entries.Keys;
            foreach (var key in allKeys)
            {
                foreach (var value in _entries[key])
                {
                    yield return new KeyValuePair<TKey, TValue>(key, value);
                }
            }
        }

        #endregion


        #region Helper Methods

        protected virtual Entry GetOrNewEntryByKey(TKey key)
        {
            Entry result;
            if (!_entries.TryGetValue(key, out result))
            {
                result = new Entry(key);
                _entries.Add(key, result);
                _orderedKeys.Add(key);
            }
            return result;
        }

        protected virtual Entry GetEntryByKey(TKey key)
        {
            Entry result;
            return _entries.TryGetValue(key, out result)
                ? result
                : null;
        }

        protected virtual Entry GetEntryByIndex(int index)
        {
            return _entries[_orderedKeys[index]];
        }
        
        protected virtual int? GetEntryIndexByKey(TKey key)
        {
            for (var i = 0; i < _orderedKeys.Count; i++)
            {
                if (_keyComparer.Equals(_orderedKeys[i], key))
                {
                    return i;
                }
            }
            return null;
        }

        #endregion
    }
}