using System;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;
using System.Collections.Generic;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Patterns.AppModel
{
    public static class AppContextExtensions
    {
        public static void SetTypeResolver(this IAppContext context, ITypeResolver typeResolver)
        {
            var typeResolverContext = context as ITypeResolverAppContext;
            if (typeResolverContext == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                typeResolverContext.SetTypeResolver(typeResolver);
            }
        }

        [NotNull]
        public static ITypeResolver GetTypeResolver(this IAppContext context)
        {
            var typeResolverContext = context as ITypeResolverAppContext;
            if (typeResolverContext == null)
            {
                throw new InvalidOperationException();
            }
            
            return typeResolverContext.TypeResolver ?? throw new InvalidOperationException();
        }

        
        public static IEnumerable<object> GetValues(this IAppContext context, string name)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.GetValues(name);
        }
        public static IEnumerable<object> SetValues(this IAppContext context, string name, params object[] values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.SetValues(name, values);
        }
        public static IEnumerable<object> SetValues(this IAppContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.SetValues(name, values);
        }
        public static void AddValues(this IAppContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            storageContext.AddValues(name, values);
        }
        public static void RemoveValues(this IAppContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            storageContext.RemoveValues(name, values);
        }
        
        public static IEnumerable<T> GetValues<T>(this IAppContext context)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.GetValues<T>();
        }
        public static IEnumerable<T> SetValues<T>(this IAppContext context, params T[] values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.SetValues<T>(values);
        }
        public static IEnumerable<T> SetValues<T>(this IAppContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            return storageContext.SetValues<T>(values);
        }
        public static void AddValues<T>(this IAppContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            storageContext.AddValues<T>(values);
        }
        public static void RemoveValues<T>(this IAppContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageAppContext;
            if (storageContext == null)
            {
                throw new InvalidOperationException();
            }

            storageContext.RemoveValues<T>(values);
        }
    }
}