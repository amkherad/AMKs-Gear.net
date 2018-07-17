using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.Mapper;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Modeling.Annotations;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Modeling;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public class DefaultMapper : IMapper
    {
        public delegate bool MatchFinder(Configuration config, ExtendedProperyInfo destinationInfo, ExtendedProperyInfo sourceInfo);

        #region Mapper
        private readonly Configuration _config;
        // ReSharper disable once ConvertToAutoProperty
        public Configuration Config => _config;

        protected internal DefaultMapper(Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _config = configuration;
        }

        public object SourceToDestination(Type destType, object destination, Type srcType, object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            //if (source.GetType() != srcType) throw new InvalidOperationException();

            //var destTypeInfo = destType.GetTypeInfo();

            //if (destination != null)
            //    if (destination.GetType() != destType)
            //        throw new InvalidOperationException();

            DefaultFullMapping(ref destination, destType, destType.GetTypeInfo(), source, srcType, srcType.GetTypeInfo(), Config);
            return destination;
        }
        public object SourceToDestination(Type destType, object destination, object[] sources)
        {
            return destination;
        }
        public object SourceToDestination(Type destType, object destination, IValueResolver resolver)
        {
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void DefaultPropertyMapping(
            object destination, Type destType, TypeInfo destTypeInfo,
            object source, Type srcType, TypeInfo srcTypeInfo,
            Configuration config)
        {
            var destProperties = ModelingHelper.GetMembers(destType);
            var srcProperties = ModelingHelper.GetMembers(srcType, property => property.CanRead)
                .Select(x => new ExtendedProperyInfo(x, srcType))
                .ToArray();

            var matchFinder = config.MatchFinder;
            var skipNulls = config.SkipNulls;

            var evaluations = new List<ExtendedProperyInfo>();
            foreach (var prop in destProperties)
            {
                var destinationExtendedInfo = new ExtendedProperyInfo(prop, destType);

                if (destinationExtendedInfo.GetEvaluationRequired(srcType, source))
                {
                    evaluations.Add(destinationExtendedInfo);
                    continue;
                }
                //var rName = prop.Name;

                var srcProp = srcProperties.FirstOrDefault(x => matchFinder(config, destinationExtendedInfo, x));

                if (srcProp != null)
                {
                    var result = srcProp.MemberInfo.GetValue(source);
                    if (!skipNulls || result != null || destinationExtendedInfo.PassNullsToCast)
                    {
                        var castExpression = destinationExtendedInfo.CastExpression;
                        if (castExpression != null)
                            result = castExpression(result);
                        prop.SetValue(destination, result);
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
        protected void DefaultCollectionMapping(
            object destination, Type destType, TypeInfo destTypeInfo, Type destGenericParameter,
            object source, Type srcType, TypeInfo srcTypeInfo,
            Configuration config, bool forced)
        {
            if (destination is IDictionary)
            {
                //BUG: debug dictionary...
                var srcDict = source as ICollection;
                if (srcDict == null)
                {
                    if (forced)
                        throw new InvalidOperationException(Localization.Localization
                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                x => x.UnableToFillDestinationCollection));
                    return;
                }
                if (destTypeInfo.IsGenericType)
                {
                    var isSourceGeneric = srcTypeInfo.IsGenericType;
                    var destGenericArgs = destTypeInfo.GenericTypeArguments;
                    if (destGenericArgs.Length != 2)
                        throw new InvalidOperationException(Localization.Localization
                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                x => x.InvalidNumberOfGenericArguments));
                    var fillerMethodName = isSourceGeneric
                        ? nameof(_fillCollectionStronged)
                        : nameof(_fillCollectionWeaked);
                    var filler = typeof(DefaultMapper).GetTypeInfo().GetDeclaredMethod(fillerMethodName);
                    var srcGenericParameter = srcTypeInfo.GenericTypeArguments[0];
                    var callableMethod = filler.MakeGenericMethod(destGenericParameter, srcGenericParameter);
                    callableMethod.Invoke(this, new[] { destGenericParameter, destination, srcGenericParameter, source });
                }
                else
                {
                    var destDict = destination as IDictionary;
                    //foreach (var item in srcDict)
                    //    destDict.Add();
                }
            }
            else
            {
                var destEnum = destination as ICollection;
                var srcEnum = source as IEnumerable;
                if (destEnum == null || srcEnum == null)
                {
                    if (forced)
                        throw new InvalidOperationException(Localization.Localization
                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                x => x.UnableToFillDestinationCollection));
                    return;
                }
                if (destTypeInfo.IsGenericType)
                {
                    var isSourceGeneric = srcTypeInfo.IsGenericType;
                    var filler = typeof(DefaultMapper).GetTypeInfo().GetDeclaredMethod(
                        isSourceGeneric ? nameof(_fillCollectionStronged) : nameof(_fillCollectionWeaked));
                    var srcGenericParameter = srcTypeInfo.GenericTypeArguments[0];
                    var callableMethod = filler.MakeGenericMethod(destGenericParameter, srcGenericParameter);
                    try
                    {
                        callableMethod.Invoke(this, new[] { destGenericParameter, destination, srcGenericParameter, source });
                    }
                    catch
                    {
                        throw new Exception(Localization.Localization
                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                x => x.MapperException));
                    }
                }
                else
                {
                    var destList = destination as IList;
                    if (destList == null)
                    {
                        if (forced)
                            throw new InvalidOperationException(Localization.Localization
                                .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                    x => x.UnableToFillDestinationCollection));
                        return;
                    }
                    foreach (var item in srcEnum)
                        destList.Add(item);
                }
            }
        }
        private void _fillCollectionStronged<TDest, TSrc>(Type destType, object destObj, Type srcType, object srcObj)
        {
            var destination = (ICollection<TDest>)destObj;
            var source = (IEnumerable<TSrc>)srcObj;
            foreach (var item in source)
            {
                var result = SourceToDestination(destType, null, srcType, item);
                //var result = Mapper.Map<TDest>(item);
                destination.Add((TDest)result);
            }
        }
        private void _fillCollectionWeaked<TDest>(Type destType, object destObj, Type srcType, object srcObj)
        {
            var destination = (ICollection<TDest>)destObj;
            var source = (IEnumerable)srcObj;
            foreach (var item in source)
            {
                var result = SourceToDestination(destType, null, srcType, item);
                //var result = Mapper.Map<TDest>(item);
                destination.Add((TDest)result);
            }
        }
        private void _fillDictionaryStronged<TKey, TValue>(Type destType, object destObj, Type srcType, object srcObj)
        {
            var destination = (IDictionary<TKey, TValue>)destObj;
            var source = (IDictionary<TKey, TValue>)srcObj;
            foreach (var item in source)
            {
                var result = SourceToDestination(destType, null, srcType, item.Value);
                //var result = Mapper.Map<TDest>(item);
                destination.Add(item.Key, (TValue)result);
            }
        }
        private void _fillDictionaryWeaked<TKey, TValue>(Type destType, object destObj, Type srcType, object srcObj)
        {
            var destination = (IDictionary<TKey, TValue>)destObj;
            var source = (IDictionary)srcObj;
            foreach (KeyValuePair<TKey, TValue> item in source)
            {
                var result = SourceToDestination(destType, null, srcType, item.Value);
                //var result = Mapper.Map<TDest>(item);
                destination.Add(item.Key, (TValue)result);
            }
        }
        protected void DefaultFullMapping(
            ref object destination, Type destType, TypeInfo destTypeInfo,
            object source, Type srcType, TypeInfo srcTypeInfo,
            Configuration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            var actAsCollection = false;
            var isDestinationFilled = destination == null;
            Type destGenericParameter = null;
            if (destination == null)
            {
                actAsCollection = typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(destTypeInfo) && source is IEnumerable;
                if (actAsCollection)
                {
                    var resolvedType = TypeResolver.TryCreateInstance(destType);
                    if (resolvedType == null)
                    {
                        if (destTypeInfo.IsSubclassOf(typeof(IDictionary)))
                        {
                            var enumerableTypes = destTypeInfo.ImplementedInterfaces
                                .Where(t => t.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                                .Select(t => t.GenericTypeArguments[0])
                                .ToList();
                            var enumerableTypesCount = enumerableTypes.Count;
                            if (enumerableTypesCount > 2)
                                throw new InvalidOperationException(
                                    Localization.Localization
                                        .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                            x => x.ThereIsMoreThanOneEnumerableImplementationInDestinationType));
                            if (enumerableTypesCount == 2)
                            {
                                //destGenericParameter = enumerableTypes[0];
                                var dictType = typeof(Dictionary<,>).MakeGenericType(enumerableTypes.ToArray());
                                resolvedType = TypeResolver.TryCreateInstance(dictType);
                                if (resolvedType == null)
                                    throw new InvalidOperationException(
                                        Localization.Localization
                                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                                x => x.UnableToCreateAnInstanceOfDestination));
                            }
                            else
                            {
                                resolvedType = new WeakTypeDictionary();
                            }
                        }
                        else
                        {
                            var enumerableType = destTypeInfo.IsGenericType &&
                                                 destType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                ? destTypeInfo.GenericTypeArguments[0]
                                : destTypeInfo.ImplementedInterfaces
                                    .Where(t =>
                                    {
                                        var typeInfo = t.GetTypeInfo();
                                        return typeInfo.IsGenericType &&
                                               typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                                    })
                                    .Select(t => t.GenericTypeArguments[0])
                                    .FirstOrDefault();
                            if (enumerableType != null)
                            {
                                destGenericParameter = enumerableType;
                                var listType = typeof(List<>).MakeGenericType(enumerableType);
                                resolvedType = TypeResolver.TryCreateInstance(listType);
                                if (resolvedType == null)
                                    throw new InvalidOperationException(
                                        Localization.Localization
                                            .Format<IAutomationLocalization, DefaultAutomationLocalization>(
                                                x => x.UnableToCreateAnInstanceOfDestination));
                            }
                            else
                            {
                                resolvedType = new WeakTypeCollection();
                            }
                        }
                    }
                    destination = resolvedType;
                }
                else
                {
                    destination = TypeResolver.TryCreateInstance(destType);
                }
            }
            DefaultPropertyMapping(destination, destType, destTypeInfo, source, srcType, srcTypeInfo, config);
            if (actAsCollection)
            {
                DefaultCollectionMapping(destination, destType, destTypeInfo, destGenericParameter, source, srcType, srcTypeInfo, config, isDestinationFilled);
            }
        }
        //private void _deepCopy(object destination, object source, StringComparer comparer)
        //{
        //    throw new NotImplementedException();
        //}

        public void Dispose()
        { }
        #endregion

        #region Mapping Types
        public class ExtendedProperyInfo
        {
            public readonly IModelMemberInfo MemberInfo;
            public readonly string Name;
            public readonly Type PropertyType;
            public readonly TypeInfo PropertyTypeInfo;
            public readonly Type ObjectType;

            public readonly Attribute[] CustomAttributes;

            public Func<object, object> CastExpression;
            public bool PassNullsToCast;

            public MapperEvaluateAttribute Evaluation;
            public Func<object, object> EvaluateExpression;

            public ExtendedProperyInfo(IModelMemberInfo pInfo, Type objType)
            {
                MemberInfo = pInfo;
                ObjectType = objType;
                PropertyType = pInfo.Type.GetPureType();
                PropertyTypeInfo = PropertyType.GetTypeInfo();

                CustomAttributes = pInfo.GetCustomAttributes(true).ToArray();

                Name =
                    (CustomAttributes.FirstOrDefault(x => x is NameAttribute) as NameAttribute)
                        ?.Name ?? pInfo.Name;

                //var evals = CustomAttributes.Where(x => x is MapperEvaluateAttribute);
            }

            public bool GetEvaluationRequired(Type srcType, object source)
            {
                var evals = CustomAttributes.OfType<MapperEvaluateAttribute>()
                    .OrderByDescending(x => x.Order).ToList();

                var eval = evals.FirstOrDefault(x => x.InternalSource == srcType);

                if (eval == null)
                {
                    if (evals.Count == 0) return false;

                    eval = evals.FirstOrDefault();
                }

                Evaluation = eval;
                EvaluateExpression = eval.EvaluateExpression;

                return true;
            }
        }

        [Immutable]
        public class Configuration
        {
            public static readonly MatchFinder DefaultMatchFinder = (config, destInfo, srcInfo) =>
            {
                if (!config.NameComparer.Equals(srcInfo.Name, destInfo.Name)) return false;

                var srcAttrs = srcInfo.MemberInfo.GetCustomAttributes(true);
                var dstAttrs = destInfo.MemberInfo.GetCustomAttributes(true);

                var annotationExcludes = config.SourceExcludeMember;
                if (annotationExcludes != null && annotationExcludes.Count > 0)
                {
                    foreach (var attr in srcAttrs)
                        if (annotationExcludes.Any(x => x._IsInstanceOfType(attr)))
                            return false;
                    foreach (var attr in dstAttrs)
                        if (annotationExcludes.Any(x => x._IsInstanceOfType(attr)))
                            return false;
                }

                if (srcAttrs.Any(x => x is MapperExcludeAttribute)) return false;
                if (dstAttrs.Any(x => x is MapperExcludeAttribute)) return false;

                var propertyType = srcInfo.PropertyType;
                var propertyTypeInfo = srcInfo.PropertyTypeInfo;

                var assignable = destInfo.PropertyTypeInfo.IsAssignableFrom(propertyTypeInfo);

                if (assignable) return true;

                var castAttr = dstAttrs.FirstOrDefault(x => x is MapperCastAttribute) as MapperCastAttribute;
                if (castAttr == null) return false;

                var source = castAttr.InternalSource;
                if (source == null || propertyType == source)
                {
                    destInfo.CastExpression = castAttr.InternalCastExpression;
                    destInfo.PassNullsToCast = castAttr.InternalPassNulls;
                    return true;
                }

                return false;
            };

            public MatchFinder MatchFinder { get; }
            public StringComparer NameComparer { get; }
            public bool SkipNulls { get; protected set; }

            protected readonly IList<Type> SourceExcludeMember;
            public IReadOnlyCollection<Type> SourceMemberExcludeAnnotation => new ReadOnlyCollection<Type>(SourceExcludeMember);

            public Configuration(MatchFinder matchFinder, StringComparer nameComparer, IEnumerable<Type> sourceExcludeMembers)
            {
                if (matchFinder == null) throw new ArgumentNullException(nameof(matchFinder));
                if (nameComparer == null) throw new ArgumentNullException(nameof(nameComparer));
                if (sourceExcludeMembers == null) throw new ArgumentNullException(nameof(sourceExcludeMembers));

                MatchFinder = matchFinder;
                NameComparer = nameComparer;
                SourceExcludeMember = sourceExcludeMembers.ToList();
            }
            public Configuration(MatchFinder matchFinder, StringComparer nameComparer)
                : this(matchFinder, nameComparer, new List<Type>()) { }
            public Configuration(IEnumerable<Type> sourceExcludeMembers)
                : this(DefaultMatchFinder, StringComparer.CurrentCultureIgnoreCase, sourceExcludeMembers) { }


            //public Configuration AddSourceMemberExcludeAnnotation(Type annotationType)
            //{
            //    if(annotationType == null) throw new ArgumentNullException(nameof(annotationType));
            //    SourceExcludeMember.Add(annotationType);
            //    return this;
            //}


            public static Configuration Default { get; private set; } =
                new Configuration(DefaultMatchFinder, StringComparer.CurrentCultureIgnoreCase)
                {
                    SkipNulls = true
                };
            public static Configuration CaseInsensetiveMapping { get; private set; } =
                new Configuration(DefaultMatchFinder, StringComparer.CurrentCultureIgnoreCase)
                {
                    SkipNulls = true
                };
            public static Configuration CaseSensetiveMapping { get; private set; } =
                new Configuration(DefaultMatchFinder ?? DefaultMatchFinder, StringComparer.CurrentCulture)
                {
                    SkipNulls = true
                };

            public static Configuration CreateCaseInsensetiveMapping(MatchFinder matchFinder = null)
            {
                return new Configuration(matchFinder ?? DefaultMatchFinder, StringComparer.CurrentCultureIgnoreCase)
                {
                    SkipNulls = true
                };
            }
            public static Configuration CreateCaseSensetiveMapping(MatchFinder matchFinder = null)
            {
                return new Configuration(matchFinder ?? DefaultMatchFinder, StringComparer.CurrentCulture)
                {
                    SkipNulls = true
                };
            }
            //public static void OverrideDefault()
            //{

            //}
        }
        #endregion
    }

    //public class XX
    //{
    //    public void Get()
    //    {
    //        var method = GetType().GetRuntimeMethod(nameof(_get), new Type[0]);


    //        GC.KeepAlive(method);
    //    }
    //    private void _get() { }
    //}
}