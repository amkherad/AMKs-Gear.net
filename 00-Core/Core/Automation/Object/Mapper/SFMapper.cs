using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Automation;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Modeling;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public class SFMapper : IMapper
    {
        public MapperContext Context { get; }
        public MapperCacheContext CacheContext { get; }

        public class MapperCacheContext : TypeCacheContext<MappingProperyInfo[]>
        {

        }

        public SFMapper(MapperContext context)
        {
            Context = context;
            CacheContext = new MapperCacheContext();
        }
        public SFMapper(MapperContext context, MapperCacheContext cacheContext)
        {
            Context = context;
            CacheContext = cacheContext;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool _isDeepMappingStrategy(Type type, TypeInfo typeInfo)
        {
            if (typeInfo.IsPrimitive || type == typeof(string) || typeInfo.IsValueType)
                return false;
            return true;
        }

        public object SourceToDestination(Type destType, object destination, Type srcType, object source)
        {
            _map(CacheContext, ref destination, destType, destType.GetTypeInfo(), source, srcType, srcType.GetTypeInfo(),
                Context, 0, 100
                );
            return destination;
            //throw new NotImplementedException();
        }

        public object SourceToDestination(Type destType, object destination, object[] sources)
        {
            throw new NotImplementedException();
        }

        public object SourceToDestination(Type destType, object destination, IValueResolver resolver)
        {
            throw new NotImplementedException();
        }

        private void _map(MapperCacheContext cacheContext,
            ref object destination, Type destType, TypeInfo destTypeInfo,
            object source, Type srcType, TypeInfo srcTypeInfo,
            MapperContext context, int cLevel, int topLevel)
        {
            //if (dest == null) throw new ArgumentNullException(nameof(dest));
            if (context == null) throw new ArgumentNullException(nameof(context));
            var actAsCollection = false;
            var isDestinationFilled = destination == null;
            Type destGenericParameter = null;
            var resolver = context.TypeResolver;
            if (destination == null)
            {
                actAsCollection = typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(destTypeInfo) && source is IEnumerable;
                if (actAsCollection)
                {
                    object resolvedType = null;
                    if (destTypeInfo.IsClass)
                    {
                        resolvedType = resolver.Resolve(destType);
                    }
                    else
                    {
                        bool useAsGenericCollection = false;
                        Type[] genericCollectionParameterTypes = null;

                        bool useAsGenericDictionary = false;
                        Type[] genericDictionaryParameterTypes = null;
                        foreach (var gDestTypeInterface in destTypeInfo.ImplementedInterfaces)
                        {
                            var tpInfo = gDestTypeInterface.GetGenericTypeDefinition().GetTypeInfo();
                            if (tpInfo.IsSubclassOf(typeof(IDictionary<,>)))
                            {
                                useAsGenericDictionary = true;
                                genericDictionaryParameterTypes = tpInfo.GenericTypeArguments;
                                break;
                            }
                            else if (!useAsGenericCollection && tpInfo.IsSubclassOf(typeof(IEnumerable<>)))
                            {
                                useAsGenericCollection = true;
                                genericCollectionParameterTypes = tpInfo.GenericTypeArguments;
                            }
                        }

                        if (useAsGenericDictionary)
                        {
                            var dictType = typeof(Dictionary<,>).MakeGenericType(genericDictionaryParameterTypes);
                            resolvedType = resolver.Resolve(dictType);
                            if (resolvedType == null)
                                throw new InvalidOperationException(
                                    LocalizationFramework.Localization
                                        .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                            x => x.UnableToCreateAnInstanceOfDestination));
                        }
                        else if (destTypeInfo.IsSubclassOf(typeof(IDictionary)))
                        {
                            resolvedType = new WeakTypeDictionary();
                        }
                        if (useAsGenericCollection)
                        {
                            var listType = typeof(List<>).MakeGenericType(genericCollectionParameterTypes);
                            resolvedType = resolver.Resolve(listType);
                            if (resolvedType == null)
                                throw new InvalidOperationException(
                                    LocalizationFramework.Localization
                                        .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                            x => x.UnableToCreateAnInstanceOfDestination));
                        }
                        else
                        {
                            resolvedType = new WeakTypeCollection();
                        }
                    }
                    destination = resolvedType;
                }
                else
                {
                    destination = resolver.Resolve(destType);
                }
            }

            DefaultPropertyMapping(cacheContext,
                destination, destType, destTypeInfo,
                source, srcType, srcTypeInfo,
                context, cLevel, topLevel);

            if (actAsCollection)
            {
                DefaultCollectionMapping(cacheContext,
                    destination, destType, destTypeInfo, destGenericParameter, source, srcType, srcTypeInfo, context,
                    cLevel, topLevel, isDestinationFilled);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void DefaultPropertyMapping(MapperCacheContext cacheContext,
            object destination, Type destType, TypeInfo destTypeInfo,
            object source, Type srcType, TypeInfo srcTypeInfo,
            MapperContext context, int cLevel, int topLevel)
        {
            MappingProperyInfo[] destPropertyInfos;
            MappingProperyInfo[] srcPropertyInfos;
            var destProperties = cacheContext.GetState(destType, out destPropertyInfos)
                ? destPropertyInfos
                : ModelingHelper.GetMembers(destType).Select(x => new MappingProperyInfo(x, destType));
            var srcProperties = cacheContext.GetState(srcType, out srcPropertyInfos)
                ? srcPropertyInfos
                : ModelingHelper.GetMembers(srcType, property => property.CanRead)
                .Select(x => new MappingProperyInfo(x, srcType))
                .ToArray();

            var matchFinder = context.MatchFinder;
            var skipNulls = context.SkipNulls;

            var evaluations = new List<MappingProperyInfo>();
            foreach (var destProp in destProperties)
            {
                if (destProp.IsEvaluationRequired(srcType, source))
                {
                    evaluations.Add(destProp);
                    continue;
                }
                //var rName = prop.Name;

                var srcProp = srcProperties.FirstOrDefault(x => matchFinder(context, destProp, x));

                if (srcProp != null)
                {
                    var result = srcProp.MemberInfo.GetValue(source);
                    if (!skipNulls || result != null || destProp.PassNullsToCast)
                    {
                        if (_isDeepMappingStrategy(destProp.PropertyType, destProp.PropertyTypeInfo) &&
                        cLevel < topLevel)
                        {
                            object destPropBuffer = null;
                            _map(cacheContext,
                                ref destPropBuffer, destProp.PropertyType, destProp.PropertyTypeInfo,
                                result, srcProp.PropertyType, srcProp.PropertyTypeInfo,
                                context, cLevel + 1, topLevel);
                        }
                        else
                        {
                            var castExpression = destProp.CastExpression;
                            if (castExpression != null)
                                result = castExpression(result);
                            destProp.MemberInfo.SetValue(destination, result);
                        }
                    }
                }
            }

            foreach (var prop in evaluations)
            {
                var result = prop.EvaluateExpression(source);

                if (!skipNulls || result != null)
                {
                    prop.MemberInfo.SetValue(destination, result);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void DefaultCollectionMapping(MapperCacheContext cacheContext,
            object destination, Type destType, TypeInfo destTypeInfo, Type destGenericParameter,
            object source, Type srcType, TypeInfo srcTypeInfo,
            MapperContext context, int cLevel, int topLevel, bool forced)
        {

        }

        public void Dispose()
        {

        }
    }
}