using System;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Automation.IoC.Exceptions;

namespace AMKsGear.Core.Automation.IoC
{
    public static class TypeResolver
    {
        internal static readonly DefaultTypeResolver DefaultTypeResolver;
        private static readonly TypeResolverWrapper DefaultTypeResolverWrapper;
        private static ITypeResolver _resolver;

        static TypeResolver()
        {
            DefaultTypeResolver = new DefaultTypeResolver();
            DefaultTypeResolverWrapper = new TypeResolverWrapper(DefaultTypeResolver);
        }

        public static void SetWideResolver([CanBeNull] ITypeResolver resolver)
        {
            _resolver = resolver;
        }

        [NotNull]
        public static ITypeResolver Current => _resolver ?? DefaultTypeResolverWrapper;
        [NotNull]
        public static ITypeResolver Default => DefaultTypeResolverWrapper;

        #region CreateInstance
        [CanBeNull]
        public static T CreateInstance<T>() => (_resolver ?? DefaultTypeResolver).Resolve<T>();
        [CanBeNull]
        public static object CreateInstance(Type type) => (_resolver ?? DefaultTypeResolver).Resolve(type);
        [CanBeNull]
        public static T CreateInstance<T>(params object[] parameters) => (_resolver ?? DefaultTypeResolver).Resolve<T>(parameters);
        [CanBeNull]
        public static object CreateInstance(Type type, params object[] parameters)
            => (_resolver ?? DefaultTypeResolver).Resolve(type, parameters);
        #endregion

        #region EnsureCreateInstance
        [NotNull]
        public static T EnsureCreateInstance<T>()
        {
            var result = (_resolver ?? DefaultTypeResolver).Resolve<T>();
            if (result == null) throw new TypeResolverNullResultException();
            return result;
        }
        [NotNull]
        public static object EnsureCreateInstance(Type type)
        {
            var result = (_resolver ?? DefaultTypeResolver).Resolve(type);
            if (result == null) throw new TypeResolverNullResultException();
            return result;
        }

        [NotNull]
        public static T EnsureCreateInstance<T>(params object[] parameters)
        {
            var result = (_resolver ?? DefaultTypeResolver).Resolve<T>(parameters);
            if (result == null) throw new TypeResolverNullResultException();
            return result;
        }
        [NotNull]
        public static object EnsureCreateInstance(Type type, params object[] parameters)
        {
            var result = (_resolver ?? DefaultTypeResolver).Resolve(type, parameters);
            if (result == null) throw new TypeResolverNullResultException();
            return result;
        }
        #endregion

        #region TryCreateInstance
        [CanBeNull]
        public static T TryCreateInstance<T>()
        {
            try
            {
                return (_resolver ?? DefaultTypeResolver).Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }
        [CanBeNull]
        public static object TryCreateInstance(Type type)
        {
            try
            {
                return (_resolver ?? DefaultTypeResolver).Resolve(type);
            }
            catch
            {
                return null;
            }
        }

        [CanBeNull]
        public static T TryCreateInstance<T>(params object[] parameters)
        {
            try
            {
                return (_resolver ?? DefaultTypeResolver).Resolve<T>(parameters);
            }
            catch
            {
                return default(T);
            }
        }
        [CanBeNull]
        public static object TryCreateInstance(Type type, params object[] parameters)
        {
            try
            {
                return (_resolver ?? DefaultTypeResolver).Resolve(type, parameters);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}