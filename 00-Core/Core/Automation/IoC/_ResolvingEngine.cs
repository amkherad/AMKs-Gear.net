using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Automation.IoC.Annotations;
using AMKsGear.Architecture.Modeling.Annotations;
using AMKsGear.Core.Automation.IoC.Options;
using AMKsGear.Core.Automation.Reflection;
using AMKsGear.Core.Trace;

namespace AMKsGear.Core.Automation.IoC
{
    internal enum ValueResolvingSourceHint
    {
        DefaultValue,

        ValueProvider,
        NamedConstants,
        TypedConstants,

        Mappings,
        CurrentContextMappings,

        ParameterOptionalDefaultValue,

        DefaultConstructor,
    }

    internal static class _ResolvingEngine
    {
        public static object CreateInstance(this TypeResolverTypeMapping mapping,
            ITypeResolver resolver, Type target, object[] args)
        {
            var opts = new List<TypeResolverOption>();
            if (args != null)
            {
                opts.AddRange(args.OfType<ConstantNamedValue>());
                opts.AddRange(args.OfType<ConstantTypedValue>());
                opts.AddRange(args.OfType<ValueProvider>());
            }

            return _createInstance(mapping, resolver, target, opts);
        }

        private static object _createInstance(this TypeResolverTypeMapping mapping,
            ITypeResolver resolver, Type target, List<TypeResolverOption> options)
        {
            var context = mapping.GetContext(target);
            //if (context == null)
            //{
            //    if (mapping.AllowResolveDefaults)
            //    {
            //        var opts = new List<TypeResolverOption>();
            //        if (args != null)
            //        {
            //            opts.AddRange(args.OfType<ConstantNamedValue>());
            //            opts.AddRange(args.OfType<ConstantTypedValue>());
            //            opts.AddRange(args.OfType<ValueProvider>());
            //        }
            //        var result = _recursiveResolve(new ResolvingContextExpandingHelper(mapping, context, opts),
            //            mapping, resolver, context, target, opts);
            //
            //        Logger.Write(
            //            $"The requested type for construction is not registered in any mapping, type:{target.FullName}",
            //            category: IoCNamespaceOptions.LoggerCategory);
            //
            //        return result;
            //        //if (target.GetTypeInfo().DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
            //        //return TypeResolver.Default.Resolve(target);
            //        //
            //        //return null;
            //    }
            //}

            var cacheInstance = context.Instance;
            if (cacheInstance != null)
            {
                //var lifeSpan = context.ResolveLifespan;
                //if (lifeSpan == -1) return cacheInstance;
                return cacheInstance;
            }

            object result;
            switch (context.MappingType)
            {
                case MappingType.Singleton:
                {
                    lock (context)
                    {
                        var lazyInstance = context.LazyInstance;
                        if (lazyInstance != null)
                        {
                            result = lazyInstance.GetValue();
                            context.Instance = result;
                        }
                        else
                            result = cacheInstance;
                    }

                    break;
                }
                case MappingType.Factory:
                {
                    var cacheFactory = context.FactoryCache;
                    try
                    {
                        if (cacheFactory)
                        {
                            Monitor.Enter(context);
                        }

                        var f1 = context.Factory1;
                        var instance = f1 != null
                            ? f1()
                            : context.Factory2(target);
                        if (cacheFactory)
                            context.Instance = instance;

                        result = instance;
                    }
                    finally
                    {
                        if (cacheFactory)
                        {
                            Monitor.Exit(context);
                        }
                    }

                    break;
                }
                case MappingType.Resolve:
                {
                    var opts = context.Options.OfType<TypeResolverOption>().ToList();

                    opts.AddRange(options);

                    var helperContext = new ResolvingContextExpandingHelper(opts);

                    result = _recursiveResolve(
                        helperContext: helperContext,
                        mapping: mapping,
                        resolver: resolver,
                        context: context,
                        target: target,
                        @override: null,
                        options: opts);

                    //#### _resolveProperties moved inside _recursiveResolve. #######################
                    //_resolveProperties(result,
                    //    helperContext,
                    //    mapping,
                    //    context,
                    //    target,
                    //    opts);

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var appliers = context.ApplierCallbacks;
            if (appliers != null)
            {
                foreach (var applier in appliers)
                {
                    applier?.ApplyBindings(
                        result,
                        resolver,
                        mapping,
                        context,
                        options
                    );
                }
            }

            var queryContextList = options.OfType<IoCQueryContext>();
            foreach (var query in queryContextList)
                query.SetContext(context);
            var queryContextListInternal = options.OfType<_Internal_IoCQueryContext>();
            foreach (var query in queryContextListInternal)
                query.SetContext(context);

            return result;
        }

        public static bool CanResolve(this TypeResolverTypeMapping mapping, ITypeResolver resolver, Type target,
            object[] args)
        {
            var context = mapping.GetContext(target);
            if (context == null) return false;

            switch (context.MappingType)
            {
                case MappingType.Singleton:
                case MappingType.Factory:
                    return true;

                case MappingType.Resolve:
                //var opts = args.Where(o => o is TypeResolverOption).Cast<TypeResolverOption>().ToList();
                //return _recursiveCanResolve(new ResolvingContextExpandingHelper(mapping, context, opts),
                //    mapping, context, target, opts);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private class ConstructorSelectorParameterContext
        {
            public readonly ParameterInfo ParameterInfo;
            public readonly List<TypeResolverAttribute> Attributes;
            public ValueResolvingSourceHint ResolvingHint;
            public bool IsDefaultConstructorHint;
            public readonly string Name;
            public bool UseDefaultValue = false;

            public ValueProvider ValueProvider;

            public ConstructorSelectorParameterContext(ParameterInfo pInfo)
            {
                ParameterInfo = pInfo;
                Attributes = pInfo.GetCustomAttributes<TypeResolverAttribute>(true).ToList();
                Name = pInfo.GetCustomAttribute<NameAttribute>(true)?.Name ?? pInfo.Name;
            }
        }

        private class ConstructorSelectorContext
        {
            public ConstructorInfo ConstructorInfo;
            public List<ConstructorSelectorParameterContext> Parameters;
            public IEnumerable<TypeResolverAttribute> Attributes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ConstructorSelectorContext _getConstructor(
            TypeResolverTypeMapping mapping,
            ResolvingContextExpandingHelper context,
            Type requestedType, TypeInfo requestedTypeInfo)
        {
            var constructors =
                requestedTypeInfo.DeclaredConstructors
                    .Select(x =>
                        new ConstructorSelectorContext
                        {
                            ConstructorInfo = x,
                            Parameters = x.GetParameters()
                                .Select(pInfo => new ConstructorSelectorParameterContext(pInfo)).ToList(),
                            Attributes = x.GetCustomAttributes<TypeResolverAttribute>(true)
                        })
                    .Where(x => !x.Attributes.Any(a => a is ResolveSkipAttribute))
                    .OrderByDescending(
                        x => x.Attributes.OfType<ResolveOrderAttribute>().FirstOrDefault()?.ResolveOrder ?? 0);

            return constructors
                .FirstOrDefault(c =>
                    c.Parameters.All(x => _canResolveParameter(mapping, context, x)));
        }

        private static bool _canResolveParameter(
            TypeResolverTypeMapping mapping,
            ResolvingContextExpandingHelper context,
            ConstructorSelectorParameterContext parameterInfo)
        {
            var pInfo = parameterInfo.ParameterInfo;
            var useDefaultValue = parameterInfo.Attributes.Any(x => x is ResolveDefaultValueAttribute);
            parameterInfo.UseDefaultValue = useDefaultValue;

            if (context.CurrentContextMapping.TypeExists(pInfo.ParameterType))
            {
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.CurrentContextMappings;
                return true;
            }

            var vP = context.ValueProviders.FirstOrDefault(x => x.CanProvide(
                TypeResolverDestinationType.ConstructorParameter,
                pInfo.ParameterType, parameterInfo.Name));
            if (vP != null)
            {
                parameterInfo.ValueProvider = vP;
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.ValueProvider;
                return true;
            }

            if (context.NamedValues.Any(x => x.ParameterName == parameterInfo.Name))
            {
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.NamedConstants;
                return true;
            }

            if (context.TypedValues.Any(x => x.ParameterType == pInfo.ParameterType))
            {
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.TypedConstants;
                return true;
            }

            if (mapping.TypeExists(pInfo.ParameterType))
            {
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.Mappings;
                return true;
            }

            if (pInfo.HasDefaultValue)
            {
                parameterInfo.ResolvingHint = ValueResolvingSourceHint.ParameterOptionalDefaultValue;
                return true;
            }

            if (mapping.AllowResolveDefaults)
            {
                var pType = pInfo.ParameterType;
                var pTypeInfo = pType.GetTypeInfo();
                var pConstructors = pTypeInfo.DeclaredConstructors.ToList();
                if (pConstructors.Count == 0)
                {
                    parameterInfo.ResolvingHint = ValueResolvingSourceHint.DefaultValue;
                    return useDefaultValue;
                }
                else if (pConstructors.Any(x => x.GetParameters().Length == 0))
                {
                    parameterInfo.ResolvingHint = ValueResolvingSourceHint.DefaultConstructor;
                    parameterInfo.IsDefaultConstructorHint = true;
                    return true;
                }
                else
                {
                    var result = pConstructors
                        .Any(c =>
                            c.GetParameters().All(x => _canResolveParameter(
                                mapping: mapping,
                                context: context,
                                parameterInfo: new ConstructorSelectorParameterContext(x))));
                    if (result)
                    {
                        parameterInfo.ResolvingHint = ValueResolvingSourceHint.DefaultConstructor;
                        parameterInfo.IsDefaultConstructorHint = false;
                    }

                    return result;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _resolveProperties(object instance,
            ResolvingContextExpandingHelper helperContext,
            TypeResolverTypeMapping mapping,
            TypeResolverTypeMappingContext context,
            Type target, List<TypeResolverOption> options)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object _recursiveResolve(
            ResolvingContextExpandingHelper helperContext, TypeResolverTypeMapping mapping, ITypeResolver resolver,
            TypeResolverTypeMappingContext context, Type target, Type @override, List<TypeResolverOption> options)
        {
            var requestedType = @override ?? context.ToType ?? context.FromType;
            var requestedTypeInfo = requestedType.GetTypeInfo();
            if (requestedTypeInfo.IsInterface)
            {
                Logger.Default.Log(
                    $"Unable to create an interface, target:{target.FullName}, resolvingType:{requestedType.FullName}");
                return null;
            }

            if (requestedTypeInfo.IsAbstract)
            {
                Logger.Default.Log(
                    $"Unable to create an abstract class, target:{target.FullName}, resolvingType:{requestedType.FullName}");
                return null;
            }

            var constructor = _getConstructor(
                mapping: mapping,
                context: helperContext,
                requestedType: requestedType,
                requestedTypeInfo: requestedTypeInfo);
            if (constructor == null)
            {
                Logger.Default.Log(
                    $"No suitable constructor found, target:{target.FullName}, resolvingType:{requestedType.FullName}");
                return null;
            }

            var parameters = new List<object>();
            {
                foreach (var p in constructor.Parameters)
                {
                    var pInfo = p.ParameterInfo;
                    var pType = pInfo.ParameterType;
                    if (pType == target)
                    {
                        Logger.Default.Log(
                            $"Circular dependency detected, parameter:{pInfo.Name}({p.Name}) from target:{target.FullName}, resolvingType:{requestedType.FullName}");
                        return null;
                    }

                    switch (p.ResolvingHint)
                    {
                        case ValueResolvingSourceHint.CurrentContextMappings:
                        {
                            object objInstance = null;
                            if (helperContext.CurrentContextMapping != null)
                            {
                                var objContext = helperContext.CurrentContextMapping.GetContext(pType);
                                objInstance = objContext.Instance;
                                if (objInstance != null)
                                {
                                    parameters.Add(objInstance);
                                }
                                else
                                {
                                    objInstance = helperContext.CurrentContextMapping.CreateInstance(resolver,
                                        pType, options.Cast<object>().ToArray());
                                    if (objInstance != null)
                                    {
                                        parameters.Add(objInstance);
                                    }
                                }
                            }

                            if (objInstance == null)
                            {
                                Logger.Default.Log(
                                    $"Failed to resolve parameter, parameter:{pInfo.Name}({p.Name}) from target:{pType.FullName}, resolvingType:{requestedType.FullName}");
                                return null;
                            }

                            break;
                        }
                        case ValueResolvingSourceHint.ValueProvider:
                            var value = p.ValueProvider.ValueProviderFactory(
                                TypeResolverDestinationType.ConstructorParameter,
                                pType, p.Name);
                            //if (value == null) return null;
                            parameters.Add(value);
                            break;
                        case ValueResolvingSourceHint.NamedConstants:
                            parameters.Add(helperContext.NamedValues.First(x => x.ParameterName == p.Name)
                                .ParameterValue);
                            break;
                        case ValueResolvingSourceHint.TypedConstants:
                            parameters.Add(
                                helperContext.TypedValues.First(x => x.ParameterType == pType).ParameterValue);
                            break;
                        case ValueResolvingSourceHint.Mappings:
                        {
                            var resolved = _createInstance(mapping, resolver, pType, options);
                            if (resolved == null)
                            {
                                Logger.Default.Log(
                                    $"Failed to resolve parameter, parameter:{pInfo.Name}({p.Name}) from target:{pType.FullName}, resolvingType:{requestedType.FullName}");
                                return null;
                            }

                            parameters.Add(resolved);
                            break;
                        }
                        case ValueResolvingSourceHint.DefaultValue:
                            parameters.Add(Activator.CreateInstance(pType));
                            break;
                        case ValueResolvingSourceHint.ParameterOptionalDefaultValue:
                            parameters.Add(pInfo.DefaultValue);
                            break;
                        case ValueResolvingSourceHint.DefaultConstructor:
                        {
                            var resolved = p.IsDefaultConstructorHint
                                ? TypeResolver.Default.Resolve(pType)
                                : _createInstance(mapping, resolver, pType, options);

                            if (resolved == null)
                            {
                                if (p.UseDefaultValue)
                                    parameters.Add(Activator.CreateInstance(pType));
                                    else
                                {
                                    Logger.Default.Log(
                                        $"Failed to resolve parameter using default constructor, parameter:{pInfo.Name}({p.Name}) from target:{pType.FullName}, resolvingType:{requestedType.FullName}"
                                    );
                                    return null;
                                }
                            }
                            else
                            {
                                parameters.Add(resolved);
                            }

                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            object instance;
            try
            {
                instance = constructor.ConstructorInfo.Invoke(parameters.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Default.Log(
                    $"An error has been occured when try to invoke constructor, target:{target.FullName}, resolvingType:{requestedType.FullName}" +
                    Environment.NewLine +
                    $"Original exception:{ex}");
                throw;
            }

            _resolveProperties(instance, helperContext, mapping, context, target, options);

            return instance;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _recursiveCanResolve(TypeResolverTypeMapping mapping,
            TypeResolverTypeMappingContext context, Type target, List<TypeResolverOption> options)
        {
            return false;
        }
    }

    internal class ResolvingContextExpandingHelper
    {
        public readonly TypeResolverTypeMapping CurrentContextMapping;
        public readonly List<TypeResolverOption> Options;

        public readonly List<ValueProvider> ValueProviders;
        public readonly List<ConstantNamedValue> NamedValues;
        public readonly List<ConstantTypedValue> TypedValues;

        public ResolvingContextExpandingHelper(List<TypeResolverOption> options)
        {
            CurrentContextMapping = new TypeResolverTypeMapping();
            Options = options;

            ValueProviders = options.OfType<ValueProvider>().ToList();
            NamedValues = options.OfType<ConstantNamedValue>().ToList();
            TypedValues = options.OfType<ConstantTypedValue>().ToList();
        }
    }
}