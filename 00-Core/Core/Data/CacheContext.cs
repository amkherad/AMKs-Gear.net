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

        public void Clear() => _contents.Clear();

        public TContent GetOrDefault(TContentDescriptor key)
        {
            return _contents.ContainsKey(key)
                ? _contents[key]
                : default(TContent);
        }

        public TContent Get(TContentDescriptor key)
        {
            return _contents[key];
        }

        public bool TryGet(TContentDescriptor key, out TContent content)
        {
            var result = _contents.ContainsKey(key);
            content = result ? _contents[key] : default(TContent);
            return result;
        }

        public bool Exists(TContentDescriptor key)
        {
            return _contents.ContainsKey(key);
        }

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

        public bool Miss(TContentDescriptor key)
        {
            if (!_contents.ContainsKey(key))
                return false;

            _contents.Remove(key);
            return true;
        }

        public void Dispose()
        {
            //Clear(); //GC is faster than clearing.
        }
    }
}