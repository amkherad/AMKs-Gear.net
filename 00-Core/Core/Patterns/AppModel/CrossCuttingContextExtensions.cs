using System;
using System.Collections;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;
using System.Collections.Generic;

namespace AMKsGear.Core.Patterns.AppModel
{
    public static class CrossCuttingContextExtensions
    {
        #region TypeResolver
        public static void SetTypeResolver(this ICrossCuttingContext context, ITypeResolver typeResolver)
        {
            var typeResolverContext = context as ITypeResolverCrossCuttingContext;
            if (typeResolverContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                typeResolverContext.SetTypeResolver(typeResolver);
            }
        }

        public static ITypeResolver GetTypeResolver(this ICrossCuttingContext context)
        {
            var typeResolverContext = context as ITypeResolverCrossCuttingContext;
            if (typeResolverContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return typeResolverContext.GetTypeResolver();
            }
        }
        #endregion

        #region Storage
        #region Named Values
        
        public static IEnumerable<object> GetValues(this ICrossCuttingContext context, string name)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.GetValues(name);
            }
        }
        public static IEnumerable<object> SetValues(this ICrossCuttingContext context, string name, params object[] values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.SetValues(name, values);
            }
        }
        public static IEnumerable<object> SetValues(this ICrossCuttingContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.SetValues(name, values);
            }
        }
        public static IEnumerable<object> AddValues(this ICrossCuttingContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.AddValues(name, values);
            }
        }
        public static IEnumerable<object> RemoveValues(this ICrossCuttingContext context, string name, IEnumerable<object> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.RemoveValues(name, values);
            }
        }
        #endregion
        #region Typed Values
        
        public static IEnumerable<T> GetValues<T>(this ICrossCuttingContext context)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.GetValues<T>();
            }
        }
        public static IEnumerable<T> SetValues<T>(this ICrossCuttingContext context, params T[] values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.SetValues<T>(values);
            }
        }
        public static IEnumerable<T> SetValues<T>(this ICrossCuttingContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.SetValues<T>(values);
            }
        }
        public static IEnumerable<T> AddValues<T>(this ICrossCuttingContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.AddValues<T>(values);
            }
        }
        public static IEnumerable<T> RemoveValues<T>(this ICrossCuttingContext context, IEnumerable<T> values)
        {
            var storageContext = context as IStorageCrossCuttingContext;
            if (storageContext == null)
            {
                throw new NotSupportedException();
            }
            else
            {
                return storageContext.RemoveValues<T>(values);
            }
        }
        #endregion
        #endregion
    }
}