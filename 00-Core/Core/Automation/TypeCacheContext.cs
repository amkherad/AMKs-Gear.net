using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation
{
    public class TypeCacheContext<TInfo> : ITypeCacheContext<TInfo>
    {
        private readonly IDictionary<Type, TInfo> _caches = new Dictionary<Type, TInfo>();

        public virtual void Dispose() { }

        public virtual void Clear() => _caches.Clear();

        public virtual bool GetState(Type type, out TInfo infos)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return _caches.TryGetValue(type, out infos);
        }

        public virtual void Store(Type type, TInfo infos)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            _caches.Add(type, infos);
        }
    }
}