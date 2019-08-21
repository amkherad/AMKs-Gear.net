using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AMKsGear.Core.Collections
{
    /// <summary>
    /// Represents a collection of key/values, it supports duplicity in keys and values.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class KeyValuesCollection<TKey, TValue> : ICollection, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, Entry> _entries;
        private List<TKey> _orderedKeys;
        private readonly IEqualityComparer<TKey> _keyComparer;
        private readonly IEqualityComparer<TValue> _valueComparer;
        private bool _removeEmptyKeys = true;

        
        public KeyValuesCollection()
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            _entries = new Dictionary<TKey, Entry>(_keyComparer);
            _orderedKeys = new List<TKey>();
        }

        public KeyValuesCollection(IEqualityComparer<TKey> comparer)
        {
            _keyComparer = comparer;
            _entries = new Dictionary<TKey, Entry>(comparer);
            _orderedKeys = new List<TKey>();
        }

        public KeyValuesCollection(int capacity)
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            _entries = new Dictionary<TKey, Entry>(capacity, _keyComparer);
            _orderedKeys = new List<TKey>(capacity);
        }

        public KeyValuesCollection(int capacity, IEqualityComparer<TKey> comparer)
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            _entries = new Dictionary<TKey, Entry>(capacity, comparer);
            _orderedKeys = new List<TKey>(capacity);
        }

        public KeyValuesCollection(IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            
            var vals = values.ToList();
            var capacity = vals.Select(v => v.Key).Distinct(_keyComparer).Count();
            
            _entries = new Dictionary<TKey, Entry>(capacity, _keyComparer);
            _orderedKeys = new List<TKey>(capacity);

            foreach (var elem in vals)
            {
                Add(elem.Key, elem.Value);
            }
        }

        public KeyValuesCollection(IDictionary<TKey, TValue> values)
        {
            var capacity = values.Keys.Count;
            
            _keyComparer = EqualityComparer<TKey>.Default;
            _entries = new Dictionary<TKey, Entry>(capacity, _keyComparer);
            _orderedKeys = new List<TKey>(capacity);

            foreach (var elem in values)
            {
                Add(elem.Key, elem.Value);
            }
        }


        /// <summary>
        /// Checks whether the key exists in the collection or not.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool ContainsKey(TKey key) => _entries.ContainsKey(key);

        /// <summary>
        /// Checks whether the key and value pair exists in the collection or not.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool ContainsKeyValue(TKey key, TValue value)
        {
            var entry = GetEntryByKey(key);

            if (entry == null) return false;

            return _valueComparer == null
                ? entry.Contains(value)
                : entry.Contains(value, _valueComparer);
        }

        /// <summary>
        /// Checks whether the key and value pair exists in the collection or not using an equality comparer for values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="valueComparer"></param>
        /// <returns></returns>
        public virtual bool ContainsKeyValue(TKey key, TValue value, IEqualityComparer<TValue> valueComparer)
        {
            var entry = GetEntryByKey(key);

            if (entry == null) return false;

            return entry.Contains(value, valueComparer);
        }


        /// <summary>
        /// Checks whether the value exists in the collection or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool ContainsValue(TValue value)
        {
            if (_valueComparer == null)
            {
                foreach (var kv in _entries)
                    if (kv.Value.Contains(value))
                        return true;
            }
            else
            {
                foreach (var kv in _entries)
                    if (kv.Value.Contains(value, _valueComparer))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the value exists in the collection or not using an equality comparer for values.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
        {
            foreach (var kv in _entries)
                if (kv.Value.Contains(value, valueComparer))
                    return true;

            return false;
        }


        /// <summary>
        /// Counts the number of values for a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int CountEntries(TKey key) => GetEntryByKey(key)?.Count ?? 0;

        public virtual IEnumerable<TValue> this[TKey key]
        {
            get { return GetEntryByKeyOrThrow(key); }
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
            get { return GetEntryByIndex(index); }
            set
            {
                var key = _orderedKeys[index];
                _entries[key] = new Entry(key, value);
            }
        }

        
        protected virtual IEnumerable<TValue> Get(TKey key) => GetEntryByKey(key);

        protected virtual void Set(TKey key, IEnumerable<TValue> values)
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

        protected virtual void Set(TKey key, TValue value) => Set(key, new[] {value});

        

        /// <summary>
        /// Replaces all values of a key with new ones.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IEnumerable<TValue> ReplaceValuesOfKey(TKey key, IEnumerable<TValue> values)
        {
            if (_entries.TryGetValue(key, out var entries))
            {
                _entries[key] = new Entry(key, values);
                return entries;
            }
            else
            {
                _entries.Add(key, new Entry(key, values));
                _orderedKeys.Add(key);
                return Enumerable.Empty<TValue>();
            }
        }

        /// <summary>
        /// Tries to get values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual bool TryGetValues(TKey key, out IEnumerable<TValue> values)
        {
            var result = _entries.TryGetValue(key, out var entries);
            values = entries;
            return result;
        }

        /// <summary>
        /// Sets all values for a key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public virtual void TryAddValues(TKey key, IEnumerable<TValue> values)
        {
            if (_entries.ContainsKey(key))
            {
                var entry = _entries[key];
                foreach (var value in values)
                {
                    if (!entry.Contains(value))
                    {
                        entry.Add(value);
                    }
                }
            }
            else
            {
                _entries.Add(key, new Entry(key, values));
                _orderedKeys.Add(key);
            }
        }

        /// <summary>
        /// Sets all values for a key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public virtual void TryAddValues(TKey key, IEnumerable<TValue> values, IEqualityComparer<TValue> valueComparer)
        {
            if (_entries.ContainsKey(key))
            {
                var entry = _entries[key];
                foreach (var value in values)
                {
                    if (!entry.Contains(value, valueComparer))
                    {
                        entry.Add(value);
                    }
                }
            }
            else
            {
                _entries.Add(key, new Entry(key, values));
                _orderedKeys.Add(key);
            }
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public virtual void Clear()
        {
            _entries = new Dictionary<TKey, Entry>();
            _orderedKeys = new List<TKey>();
        }

        /// <summary>
        /// Gets the first value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual TValue First(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.First();
        }

        /// <summary>
        /// Gets the first or default value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue FirstOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.FirstOrDefault();
        }

        /// <summary>
        /// Gets a single value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual TValue Single(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.Single();
        }

        /// <summary>
        /// Gets a single or default value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue SingleOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.SingleOrDefault();
        }

        /// <summary>
        /// Gets the last value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual TValue Last(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) throw new KeyNotFoundException();
            return entry.Last();
        }

        /// <summary>
        /// Gets the last or default value of a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue LastOrDefault(TKey key)
        {
            var entry = GetEntryByKey(key);
            if (entry == null) return default(TValue);
            return entry.LastOrDefault();
        }

        /// <summary>
        /// Adds a key value item to collection. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(TKey key, TValue value)
        {
            var col = GetOrNewEntryByKey(key);
            col.Add(value);
        }

        /// <summary>
        /// Adds a range of values for single key to collections.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="range"></param>
        public virtual void AddRange(TKey key, IEnumerable<TValue> range)
        {
            var col = GetOrNewEntryByKey(key);
            col.AddRange(range);
        }

        /// <summary>
        /// Removes a key value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Remove(TKey key, TValue value)
        {
            var col = GetEntryByKey(key);
            if (col == null)
            {
                return false;
            }

            var result = col.Remove(value);

            if (_removeEmptyKeys && col.Count == 0)
            {
                RemoveKey(key);
            }

            return result;
        }

        /// <summary>
        /// Removes all values with given key from collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public virtual void RemoveAll(TKey key, IEnumerable<TValue> values)
        {
            var col = GetEntryByKey(key);
            if (col == null)
            {
                return;
            }

            col.RemoveAll(values);

            if (_removeEmptyKeys && col.Count == 0)
            {
                RemoveKey(key);
            }
        }

        /// <summary>
        /// Removes a key and all of it's values.
        /// </summary>
        /// <param name="key"></param>
        public virtual void RemoveKey(TKey key)
        {
            _entries.Remove(key);
            _orderedKeys.RemoveAll(r => _keyComparer.Equals(r, key));
        }

        /// <summary>
        /// Removes all values using default comparer.
        /// </summary>
        /// <param name="values"></param>
        public virtual void RemoveAllValues(IEnumerable<TValue> values)
        {
            var valuesArray = values.AsArray();
            foreach (var kv in _entries)
            {
                kv.Value.RemoveAll(valuesArray);
            }
        }

        /// <summary>
        /// Removes all values using default comparer.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="valueComparer"></param>
        public virtual void RemoveAllValues(IEnumerable<TValue> values, IEqualityComparer<TValue> valueComparer)
        {
            var valuesArray = values.AsArray();
            foreach (var kv in _entries)
            {
                kv.Value.RemoveAll(valuesArray, valueComparer);
            }
        }
        

        /// <summary>
        /// Gets all keys.
        /// </summary>
        public virtual IEnumerable<TKey> Keys => _entries.Keys;
        
        /// <summary>
        /// Gets all values.
        /// </summary>
        public virtual IEnumerable<TValue> Values => _entries.SelectMany(entry => entry.Value);

        /// <summary>
        /// Returns all entries as enumerable of KeyValuePairs.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Gets or create a new entry.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Entry GetOrNewEntryByKey(TKey key)
        {
            if (!_entries.TryGetValue(key, out var result))
            {
                result = new Entry(key);
                _entries.Add(key, result);
                _orderedKeys.Add(key);
            }

            return result;
        }

        /// <summary>
        /// Gets an entry.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Entry GetEntryByKey(TKey key)
        {
            return _entries.TryGetValue(key, out var result)
                ? result
                : null;
        }

        /// <summary>
        /// Gets an entry or throws if key is not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual Entry GetEntryByKeyOrThrow(TKey key)
        {
            return _entries.TryGetValue(key, out var result)
                ? result
                : throw new KeyNotFoundException();
        }

        /// <summary>
        /// Gets an entry by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual Entry GetEntryByIndex(int index)
        {
            return _entries[_orderedKeys[index]];
        }

        /// <summary>
        /// Returns the index of an entry specified by it's key. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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


        public virtual void CopyTo(Array array, int index) => ((ICollection) _entries).CopyTo(array, index);
        public virtual int Count => _entries.Count;

        public virtual bool IsSynchronized => ((ICollection) _entries).IsSynchronized;
        public virtual object SyncRoot => ((ICollection) _entries).SyncRoot;


        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
            new Enumerator(this);

        public virtual IEnumerator GetEnumerator() => new Enumerator(this);
    }
}