using System.Collections.Generic;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data
{
    /// <summary>
    /// Simple cache service.
    /// </summary>
    /// <typeparam name="TContentDescriptor">The identity key of content to cache with.</typeparam>
    /// <typeparam name="TContent">The type of content to cache.</typeparam>
    public class CacheContext<TContentDescriptor, TContent> : ICacheContext<TContentDescriptor, TContent>
    {
        private readonly Dictionary<TContentDescriptor, TContent> _contents;

        public CacheContext()
        {
            _contents = new Dictionary<TContentDescriptor, TContent>();
        }

        public CacheContext(int capacity)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(capacity);
        }

        public CacheContext(IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(comparer);
        }

        public CacheContext(int capacity, IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(capacity, comparer);
        }

        public CacheContext(IDictionary<TContentDescriptor, TContent> dictionary)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(dictionary);
        }

        public CacheContext(IDictionary<TContentDescriptor, TContent> dictionary,
            IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(dictionary, comparer);
        }

        
        internal CacheContext(Dictionary<TContentDescriptor, TContent> dictionary, int dummy)
        {
            _contents = dictionary;
        }
        
        internal static CacheContext<TContentDescriptor, TContent> Adapt(
            Dictionary<TContentDescriptor, TContent> dictionary)
        {
            return new CacheContext<TContentDescriptor, TContent>(dictionary, 0);
        }

//        public CacheContext(IEnumerable<KeyValuePair<TContentDescriptor, TContent>> collection)
//        {
//            _contents = new Dictionary<TContentDescriptor, TContent>(collection);
//        }

//        public CacheContext(IEnumerable<KeyValuePair<TContentDescriptor, TContent>> collection,
//            IEqualityComparer<TContentDescriptor> comparer)
//        {
//            _contents = new Dictionary<TContentDescriptor, TContent>(collection, comparer);
//        }
//        

        /// <inheritdoc />
        public void Clear() => _contents.Clear();

        /// <inheritdoc />
        public void EnsureCapacity(int capacity)
        {
            //_contents.EnsureCapacity(capacity);
        }

        /// <inheritdoc />
        public TContent GetOrDefault(TContentDescriptor key)
        {
            return _contents.ContainsKey(key)
                ? _contents[key]
                : default(TContent);
        }

        /// <inheritdoc />
        public TContent Get(TContentDescriptor key)
        {
            return _contents[key];
        }

        /// <inheritdoc />
        public bool TryGetValue(TContentDescriptor key, out TContent content)
        {
            var result = _contents.ContainsKey(key);
            content = result ? _contents[key] : default(TContent);
            return result;
        }

        /// <inheritdoc />
        public bool Exists(TContentDescriptor key)
        {
            return _contents.ContainsKey(key);
        }

        /// <inheritdoc />
        public bool Cache(TContentDescriptor key, TContent content)
        {
            if (_contents.ContainsKey(key))
            {
                _contents[key] = content;
                return true;
            }

            _contents.Add(key, content);
            return false;
        }

        /// <inheritdoc />
        public int CacheAll(IDictionary<TContentDescriptor, TContent> entries)
        {
            var count = 0;
            foreach (var entry in entries)
            {
                if (_contents.ContainsKey(entry.Key))
                {
                    _contents[entry.Key] = entry.Value;
                }
                else
                {
                    _contents.Add(entry.Key, entry.Value);
                }

                ++count;
            }

            return count;
        }

        /// <inheritdoc />
        public bool Miss(TContentDescriptor key)
        {
            return _contents.Remove(key);
        }

        /// <inheritdoc />
        public int MissAll(IEnumerable<TContentDescriptor> descriptors)
        {
            var count = 0;
            foreach (var key in descriptors)
            {
                if (_contents.Remove(key))
                {
                    count++;
                }
            }
            return count;
        }

        /// <inheritdoc />
        public int Count => _contents.Count;

        /// <inheritdoc />
        public void Dispose()
        {
            
        }
    }
}