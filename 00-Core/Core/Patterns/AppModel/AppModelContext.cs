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
    /// <summary>
    /// Provides access to all key application components like IoC container, mapper, etc.
    /// </summary>
    public class AppModelContext : IAppContext,
        IStorageAppContext, ITypeResolverAppContext
    {
        private static IAppContext _instance;

        /// <summary>
        /// Singleton <see cref="AppModelContext"/>.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static IAppContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    LazyInitializer.EnsureInitialized(ref _instance, () => new AppModelContext());
                }

                return _instance;
            }
            set
            {
                if (Interlocked.CompareExchange(ref _instance, value, null) != null)
                {
                    throw new Exception("Unable to set context twice.");
                }
            }
        }


        public event StorageAppContextNamedValuesChanged NamedValuesChanged;
        public event StorageAppContextValuesChanged ValuesChanged;


        public ITypeResolver TypeResolver { get; private set; }
        private readonly IDictionary<string, ICollection<object>> _namedValues;
        private readonly IDictionary<Type, ICollection<object>> _values;


        public AppModelContext()
        {
            _namedValues = new PropertyBag<ICollection<object>>();
            _values = new PropertyBag<Type, ICollection<object>>();
        }

        protected void OnValuesChanged(Type type, IEnumerable<object> values) =>
            ValuesChanged?.Invoke(this, type, values);

        protected void OnValuesChanged<T>(IEnumerable<T> values) =>
            ValuesChanged?.Invoke(this, typeof(T), values.Cast<object>());

        protected void OnNamedValuesChanged(string name, IEnumerable<object> values) =>
            NamedValuesChanged?.Invoke(this, name, values);

        
        public void SetTypeResolver(ITypeResolver typeResolver) => TypeResolver = typeResolver;

        
        #region IStorageAppContext Members

        public IEnumerable GetNamedValues()
        {
            return _namedValues.Values.SelectMany(x => x.ToList());
        }

        public IEnumerable<object> GetValues(string name)
        {
            _namedValues.TryGetValue(name, out var result);
            return result;
        }

        public IEnumerable<object> SetValues(string name, params object[] values)
        {
            _namedValues.TryGetValue(name, out var oldValues);
            var newValues = values == null ? null : new List<object>(values);
            _namedValues[name] = newValues;
            return oldValues;
        }

        public IEnumerable<object> SetValues(string name, IEnumerable<object> values)
        {
            _namedValues.TryGetValue(name, out var oldValues);
            var newValues = values == null ? null : new List<object>(values);
            _namedValues[name] = newValues;
            return oldValues;
        }

        public IEnumerable<object> AddValues(string name, IEnumerable<object> values)
        {
            _namedValues.TryGetValue(name, out var holder);
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
            _namedValues.TryGetValue(name, out var holder);
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
            _values.TryGetValue(typeof(T), out var result);
            return result?.Cast<T>();
        }

        public IEnumerable<T> SetValues<T>(params T[] values)
        {
            _values.TryGetValue(typeof(T), out var oldValues);
            var newValues = values == null ? null : new List<object>(values.Cast<object>());
            _values[typeof(T)] = newValues;
            return oldValues?.Cast<T>();
        }

        public IEnumerable<T> SetValues<T>(IEnumerable<T> values)
        {
            _values.TryGetValue(typeof(T), out var oldValues);
            var newValues = values == null ? null : new List<object>(values.Cast<object>());
            _values[typeof(T)] = newValues;
            return oldValues?.Cast<T>();
        }

        public IEnumerable<T> AddValues<T>(IEnumerable<T> values)
        {
            _values.TryGetValue(typeof(T), out var holder);
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
            _values.TryGetValue(typeof(T), out var holder);
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
    }
}