using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.Automation.IoC
{
    public static class TypeResolverExtensions
    {
        #region Resolve
        #region Resolve
        public static object Resolve(this ITypeResolver typeResolver, Type type)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return typeResolver.Resolve(type, null, null);
        }
        public static object Resolve(this ITypeResolver typeResolver, Type type, object context)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return typeResolver.Resolve(type, context, null);
        }
        public static object Resolve(this ITypeResolver typeResolver, Type type, object[] args)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return typeResolver.Resolve(type, null, args);
        }
        public static object Resolve(this ITypeResolver typeResolver, Type type, object context, object[] args)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return typeResolver.Resolve(type, context, args);
        }
        #endregion
        #region Resolve<T>
        public static T Resolve<T>(this ITypeResolver typeResolver)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (T)typeResolver.Resolve(typeof(T), null, null);
        }
        public static T Resolve<T>(this ITypeResolver typeResolver, object context)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (T)typeResolver.Resolve(typeof(T), context, null);
        }
        public static T Resolve<T>(this ITypeResolver typeResolver, object[] args)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (T)typeResolver.Resolve(typeof(T), null, args);
        }
        public static T Resolve<T>(this ITypeResolver typeResolver, object context, object[] args)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (T)typeResolver.Resolve(typeof(T), context, args);
        }
        #endregion
        #region Resolve<TFrom, TTo>
        public static TTo Resolve<TFrom, TTo>(this IDynamicTypeResolver typeResolver)
            where TTo : TFrom
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (TTo)typeResolver.Resolve(typeof(TFrom), typeof(TTo), null, null);
        }
        public static TTo Resolve<TFrom, TTo>(this IDynamicTypeResolver typeResolver, object context)
            where TTo : TFrom
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (TTo)typeResolver.Resolve(typeof(TFrom), typeof(TTo), context, null);
        }
        public static TTo Resolve<TFrom, TTo>(this IDynamicTypeResolver typeResolver, params object[] args)
            where TTo : TFrom
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (TTo)typeResolver.Resolve(typeof(TFrom), typeof(TTo), null, args);
        }
        public static TTo Resolve<TFrom, TTo>(this IDynamicTypeResolver typeResolver, object context, params object[] args)
            where TTo : TFrom
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));

            return (TTo)typeResolver.Resolve(typeof(TFrom), typeof(TTo), context, args);
        }
        #endregion
        #endregion
        
        #region RegisterType
        public static void RegisterType<TFrom, TTo>(this ITypeMappingTypeResolverContainer container,
            params object[] options)
            where TTo : TFrom
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType(typeof(TFrom), typeof(TTo), options);
        }
        public static void RegisterType<T>(this ITypeMappingTypeResolverContainer container,
            params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType(typeof(T), options);
        }
        #endregion

        #region RegisterDictionary
        public static void RegisterDictionary(this ITypeMappingTypeResolverContainer container,
            IDictionary<Type, Type> dictionary, params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            foreach (var element in dictionary)
                container.RegisterType(element.Key, element.Value, options);
        }
        #endregion

        #region RegisterSingleton
        public static void RegisterSingleton(this ISingletonTypeResolverContainer container,
            Type from, Type to, params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            container.RegisterType(from, new TypeResolverLazy(to), options);
        }
        public static void RegisterSingleton<T>(this ISingletonTypeResolverContainer container,
            T instance, params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            
            container.RegisterType(typeof(T), instance, options);
        }
        public static void RegisterSingleton<TFrom, TTo>(this ISingletonTypeResolverContainer container,
            params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            container.RegisterType(typeof(TFrom), new TypeResolverLazy<TTo>(), options);
        }
        #endregion

        #region RegisterApplier
        public static void RegisterApplier<T>(this ITypeResolverAppliableContainer container,
            ITypeResolverApplier applier)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (applier == null) throw new ArgumentNullException(nameof(applier));

            container.RegisterApplier(typeof(T), applier);
        }
        #endregion

        #region Compiler
        public static ITypeResolver Compile(this ITypeResolverContainerMetadataExporter metaExporter)
        {
            if (metaExporter == null) throw new ArgumentNullException(nameof(metaExporter));
            
            return metaExporter as ITypeResolver;
        }
        #endregion

        //public static void RegisterSingleton(this ISingletonTypeResolverContainer container, Type type, params Type[] types)
        //{
        //    if (container == null) throw new ArgumentNullException(nameof(container));

        //    var instance = container.Resolve(type);
        //    container.RegisterType(type, instance);

        //    foreach (var t in types)
        //    {
        //        var tInstance = container.Resolve(t);
        //        container.RegisterType(t, tInstance);
        //    }
        //}
    }
}