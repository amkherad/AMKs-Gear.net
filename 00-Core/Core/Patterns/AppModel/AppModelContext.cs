using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Collections;
using System.Linq;
using AMKsGear.Architecture.Annotations;

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
        private readonly NameObjectCollection _namedValues;
        private readonly KeyValuesCollection<Type, object> _values;


        public AppModelContext()
        {
            _namedValues = new NameObjectCollection();
            _values = new KeyValuesCollection<Type, object>();
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
            return _namedValues.Values;
        }

        [NotNull]
        public IEnumerable<object> GetValues(string name)
        {
            return _namedValues.TryGetValues(name, out var values)
                ? values
                : Enumerable.Empty<object>();
        }

        public IEnumerable<object> SetValues(string name, params object[] values)
        {
            return _namedValues.ReplaceValuesOfKey(name, values);
        }

        public IEnumerable<object> SetValues(string name, IEnumerable<object> values)
        {
            return _namedValues.ReplaceValuesOfKey(name, values);
        }

        public void AddValues(string name, IEnumerable<object> values)
        {
            _namedValues.Add(name, values);
        }

        public void RemoveValues(string name, IEnumerable<object> values)
        {
            _namedValues.RemoveAll(name, values);
        }

        public IEnumerable GetTypedValues()
        {
            return _values.Values;
        }

        [NotNull]
        public IEnumerable<T> GetValues<T>()
        {
            return _values.TryGetValues(typeof(T), out var values)
                ? values.OfType<T>()
                : Enumerable.Empty<T>();
        }

        [NotNull]
        public IEnumerable GetValues(Type type)
        {
            return _values.TryGetValues(type, out var values)
                ? values
                : Enumerable.Empty<object>();
        }

        public IEnumerable<T> SetValues<T>(params T[] values)
        {
            return _values.ReplaceValuesOfKey(typeof(T), values.Cast<object>()).Cast<T>();
        }

        public IEnumerable<T> SetValues<T>(IEnumerable<T> values)
        {
            return _values.ReplaceValuesOfKey(typeof(T), values.Cast<object>()).Cast<T>();
        }

        public void AddValues<T>(IEnumerable<T> values)
        {
            _values.AddRange(typeof(T), values.Cast<object>());
        }

        public void RemoveValues<T>(IEnumerable<T> values)
        {
            _values.RemoveAll(typeof(T), values.Cast<object>());
        }

        public IEnumerable GetAllValues()
        {
            return _values.Values.Union(_namedValues.Values);
        }

        #endregion
    }
}