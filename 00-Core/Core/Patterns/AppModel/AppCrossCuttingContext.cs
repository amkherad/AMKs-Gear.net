using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Collections;
using System.Linq;

namespace AMKsGear.Core.Patterns.AppModel
{
    public class AppCrossCuttingContext : ICrossCuttingContext,
        IStorageCrossCuttingContext, ITypeResolverCrossCuttingContext
    {
        #region Singleton

        private static ICrossCuttingContext _context;

        public static ICrossCuttingContext Context
        {
            get
            {
                if (_context == null)
                {
                    LazyInitializer.EnsureInitialized(ref _context, () => new AppCrossCuttingContext());
                }
                return _context;
            }
            set
            {
                if (Interlocked.CompareExchange(ref _context, value, null) != null)
                {
                    throw new Exception("Unalbe to set context twice.");
                }
            }
        }

        #endregion

        #region IStorageCrossCuttingContext Members

        public event StorageCrossCuttingContextNamedValuesChanged NamedValuesChanged;
        public event StorageCrossCuttingContextValuesChanged ValuesChanged;

        #endregion

        #region Variables

        private ITypeResolver _typeResolver;
        private readonly IDictionary<string, ICollection<object>> _namedValues;
        private readonly IDictionary<Type, ICollection<object>> _values;

        #endregion


        public AppCrossCuttingContext()
        {
            _namedValues = new PropertyBag<ICollection<object>>();
            _values = new PropertyBag<Type, ICollection<object>>();
        }

        protected void OnValuesChanged(Type type, IEnumerable<object> values)
        {
            ValuesChanged?.Invoke(this, type, values);
        }

        protected void OnValuesChanged<T>(IEnumerable<T> values)
        {
            ValuesChanged?.Invoke(this, typeof(T), values.Cast<object>());
        }

        protected void OnNamedValuesChanged(string name, IEnumerable<object> values)
        {
            NamedValuesChanged?.Invoke(this, name, values);
        }


        #region IStorageCrossCuttingContext Members

        public IEnumerable GetNamedValues()
        {
            return _namedValues.Values.SelectMany(x => x.ToList());
        }
        
        public IEnumerable<object> GetValues(string name)
        {
            ICollection<object> result;
            _namedValues.TryGetValue(name, out result);
            return result;
        }

        public IEnumerable<object> SetValues(string name, params object[] values)
        {
            ICollection<object> oldValues;
            _namedValues.TryGetValue(name, out oldValues);
            var newValues = values == null ? null : new List<object>(values);
            _namedValues[name] = newValues;
            return oldValues;
        }

        public IEnumerable<object> SetValues(string name, IEnumerable<object> values)
        {
            ICollection<object> oldValues;
            _namedValues.TryGetValue(name, out oldValues);
            var newValues = values == null ? null : new List<object>(values);
            _namedValues[name] = newValues;
            return oldValues;
        }

        public IEnumerable<object> AddValues(string name, IEnumerable<object> values)
        {
            ICollection<object> holder;
            _namedValues.TryGetValue(name, out holder);
            if (values != null)
            {
                if (holder != null)
                {
                    holder.AddRange(values);
                }
                else
                {
                    holder = new List<object>(values);
                    _namedValues[name] = holder;
                }
            }
            return holder;
        }

        public IEnumerable<object> RemoveValues(string name, IEnumerable<object> values)
        {
            ICollection<object> holder;
            _namedValues.TryGetValue(name, out holder);
            if (holder != null && values != null)
            {
                holder.RemoveAll(values);
            }
            return holder;
        }

        public IEnumerable GetTypedValues()
        {
            return _values.Values.SelectMany(x => x.ToList());
        }

        public IEnumerable<T> GetValues<T>()
        {
            ICollection<object> result;
            _values.TryGetValue(typeof(T), out result);
            return result?.Cast<T>();
        }

        public IEnumerable<T> SetValues<T>(params T[] values)
        {
            ICollection<object> oldValues;
            _values.TryGetValue(typeof(T), out oldValues);
            var newValues = values == null ? null : new List<object>(values.Cast<object>());
            _values[typeof(T)] = newValues;
            return oldValues?.Cast<T>();
        }

        public IEnumerable<T> SetValues<T>(IEnumerable<T> values)
        {
            ICollection<object> oldValues;
            _values.TryGetValue(typeof(T), out oldValues);
            var newValues = values == null ? null : new List<object>(values.Cast<object>());
            _values[typeof(T)] = newValues;
            return oldValues?.Cast<T>();
        }

        public IEnumerable<T> AddValues<T>(IEnumerable<T> values)
        {
            ICollection<object> holder;
            _values.TryGetValue(typeof(T), out holder);
            if (values != null)
            {
                if (holder != null)
                {
                    holder.AddRange(values.Cast<object>());
                }
                else
                {
                    holder = new List<object>(values.Cast<object>());
                    _values[typeof(T)] = holder;
                }
            }
            return holder?.Cast<T>();
        }

        public IEnumerable<T> RemoveValues<T>(IEnumerable<T> values)
        {
            ICollection<object> holder;
            _values.TryGetValue(typeof(T), out holder);
            if (holder != null && values != null)
            {
                holder.RemoveAll(values.Cast<object>());
            }
            return holder?.Cast<T>();
        }

        public IEnumerable GetAllValues()
        {
            return _values.Values.SelectMany(x => x.ToList())
                .Union(
                    _namedValues.Values.SelectMany(v => v.ToList()));
        }
        
        #endregion

        #region ITypeResolverCrossCuttingContext Memebers

        public ITypeResolver GetTypeResolver() => _typeResolver;

        public void SetTypeResolver(ITypeResolver typeResolver) => _typeResolver = typeResolver;

        #endregion
    }
}