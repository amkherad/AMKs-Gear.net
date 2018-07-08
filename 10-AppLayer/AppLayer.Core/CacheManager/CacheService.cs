using System.Collections.Generic;

namespace AMKsGear.AppLayer.Core.CacheManager
{
    /// <summary>
    /// Simple cache service.
    /// </summary>
    /// <typeparam name="TContent">The type of content to cache.</typeparam>
    /// <typeparam name="TContentDescriptor">The identity key of content to cache with.</typeparam>
    public class CacheService<TContent, TContentDescriptor> : ICacheService<TContent, TContentDescriptor>
    {
        private readonly Dictionary<TContentDescriptor, TContent> _contents;

        public CacheService()
        {
            _contents = new Dictionary<TContentDescriptor, TContent>();
        }

        public CacheService(int capacity)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(capacity);
        }

        public CacheService(IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(comparer);
        }

        public CacheService(int capacity, IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(capacity, comparer);
        }

        public CacheService(IDictionary<TContentDescriptor, TContent> dictionary)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(dictionary);
        }

        public CacheService(IDictionary<TContentDescriptor, TContent> dictionary,
            IEqualityComparer<TContentDescriptor> comparer)
        {
            _contents = new Dictionary<TContentDescriptor, TContent>(dictionary, comparer);
        }

//        public CacheService(IEnumerable<KeyValuePair<TContentDescriptor, TContent>> collection)
//        {
//            _contents = new Dictionary<TContentDescriptor, TContent>(collection);
//        }

//        public CacheService(IEnumerable<KeyValuePair<TContentDescriptor, TContent>> collection,
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
            Clear();
        }
    }
}