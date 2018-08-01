using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC.LifetimeManagers;
using AMKsGear.Core.Automation.Reflection;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Automation.IoC
{
    public enum TypeResolverDestinationType
    {
        ConstructorParameter,
        Property,
        Field
    }
    public enum MappingType
    {
        Resolve,
        Singleton,
        Factory,
    }
    public enum TypeMappingCacheMode
    {
        CacheAll,
        AllowCache,
        DisallowCache
    }

    public class TypeResolverTypeMappingContext : TypeInformationTypeDescriptorContext
    {
        //public string StrongName;

        public string FromName;
        public string ToName;

        public Type FromType;
        public Type ToType;

        public Func<object> Factory1;
        public Func<Type, object> Factory2;
        public bool FactoryCache;

        public object Instance;
        public ILazyValue LazyInstance;

        public long ResolveLifespan = -1;
        public long ResolveCount = 0;

        //public long RegistrationSpinCount = 0;

        public MappingType MappingType;

        public List<ITypeResolverApplier> ApplierCallbacks;

        public readonly object[] Options;
        public readonly IIoCLifetimeManager LifetimeManager;

        public Expression<Func<object>> BuilderExpression; 
        public Func<object> BuilderFunc; 
        
        public TypeResolverTypeMappingContext(string strongName, string[] bindingNames, object[] options)
            : base(strongName)
        {
            if (bindingNames != null && bindingNames.Length > 0)
            {
                BindingNames.AddRange(bindingNames);
            }

            Options = options;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void _createKeyHashSet(
            TypeResolverTypeMappingContext context,
            out HashSet<string> uniques,
            out HashSet<string> optionals)
        {
            uniques = new HashSet<string>();
            optionals = new HashSet<string>();

            if (context.BindingStrongName != null)
                uniques.Add(context.BindingStrongName);

            if (context.FromName != null)
                uniques.Add(context.FromName);
            if (context.ToName != null)
                optionals.Add(context.ToName);

            if (context.FromType != null)
                uniques.Add(context.FromType.FullName);
            if (context.ToType != null)
                optionals.Add(context.ToType.FullName);

            foreach (var name in context.BindingNames)
            {
                uniques.Add(name);
            }
        }

        public static void UnBindUnion(TypeResolverTypeMapping mapping, TypeResolverTypeMappingContext context)
        {
            HashSet<string> uniques;
            HashSet<string> optionals;
            _createKeyHashSet(context, out uniques, out optionals);

            foreach (var key in uniques)
                mapping.Mappings.Remove(key);

            foreach (var key in optionals)
                mapping.Mappings.Remove(key);
        }
        public static void BindUnion(TypeResolverTypeMapping mapping, TypeResolverTypeMappingContext context)
        {
            HashSet<string> uniques;
            HashSet<string> optionals;
            _createKeyHashSet(context, out uniques, out optionals);

            foreach (var key in uniques)
                mapping.Mappings.Add(key, context);

            foreach (var key in optionals)
            {
                if (!mapping.Mappings.ContainsKey(key))
                    mapping.Mappings.Add(key, context);
            }
        }
    }

    public class TypeResolverTypeMapping
    {
        internal readonly IDictionary<string, TypeResolverTypeMappingContext> Mappings;
        internal bool AllowResolveDefaults = true;
        internal TypeMappingCacheMode ResolveCacheMode;

        public bool ResolveDefaults { get { return AllowResolveDefaults; } set { AllowResolveDefaults = value; } }
        public TypeMappingCacheMode CacheMode { get { return ResolveCacheMode; } set { ResolveCacheMode = value; } }

        public TypeResolverTypeMapping()
        {
            Mappings = new Dictionary<string, TypeResolverTypeMappingContext>();
        }

        public TypeResolverTypeMappingContext GetContext(Type type)
        {
            var fullName = type.FullName;
            var context = GetContext(fullName);
            if (context != null) return context;

            return AllowResolveDefaults
                ? new TypeResolverTypeMappingContext(fullName, null, new object[0])
                {
                    ToType = type,
                    ToName = type.FullName,
                    MappingType = MappingType.Resolve,
                }
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool _exists(string name, out TypeResolverTypeMappingContext context)
        {
            if (!Mappings.ContainsKey(name))
            {
                context = null;
                return false;
            }

            context = Mappings[name];
            return true;
        }

        public TypeResolverTypeMappingContext GetContext(string name)
        {
            TypeResolverTypeMappingContext context;
            _exists(name, out context);
            return context;
        }

        public bool NameExists(string name)
        {
            TypeResolverTypeMappingContext context;
            return _exists(name, out context);
        }
        public bool TypeExists(Type type)
        {
            TypeResolverTypeMappingContext context;
            return _exists(type.FullName, out context);
        }

        public void RegisterContext(TypeResolverTypeMappingContext context)
        {
            TypeResolverTypeMappingContext.BindUnion(this, context);
        }
        public TypeResolverTypeMappingContext RegisterType(Type type, object[] options, bool addToList = true)
        {
            var fullName = type?.FullName;
            var result = new TypeResolverTypeMappingContext(fullName, null, options)
            {
                ToType = type,
                ToName = fullName,
                MappingType = MappingType.Resolve
            };
            if (addToList) RegisterContext(result);
            return result;
        }
        public TypeResolverTypeMappingContext RegisterType(Type fromType, Type toType, object[] options, bool addToList = true)
        {
            var fromFullName = fromType?.FullName;
            var toFullName = toType?.FullName;
            var result = new TypeResolverTypeMappingContext(fromFullName, new[] { toFullName }, options)
            {
                FromType = fromType,
                FromName = fromFullName,

                ToType = toType,
                ToName = toFullName,

                MappingType = MappingType.Resolve
            };
            if (addToList) RegisterContext(result);
            return result;
        }
        public TypeResolverTypeMappingContext RegisterType(Type type, Func<object> factory, bool cacheInstance, object[] options, bool addToList = true)
        {
            var fullName = type?.FullName;
            var result = new TypeResolverTypeMappingContext(fullName, null, options)
            {
                ToType = type,
                ToName = fullName,

                Factory1 = factory,
                FactoryCache = cacheInstance,

                MappingType = MappingType.Factory
            };
            if (addToList) RegisterContext(result);
            return result;
        }
        public TypeResolverTypeMappingContext RegisterType(Type type, Func<Type, object> factory, bool cacheInstance, object[] options, bool addToList = true)
        {
            var fullName = type?.FullName;
            var result = new TypeResolverTypeMappingContext(fullName, null, options)
            {
                ToType = type,
                ToName = fullName,

                Factory2 = factory,
                FactoryCache = cacheInstance,

                MappingType = MappingType.Factory
            };
            if (addToList) RegisterContext(result);
            return result;
        }
        public TypeResolverTypeMappingContext RegisterType(Type type, object instance, object[] options, bool addToList = true)
        {
            var fullName = type?.FullName;
            var result = new TypeResolverTypeMappingContext(fullName, null, options)
            {
                ToType = type,
                ToName = fullName,

                Instance = instance,

                MappingType = MappingType.Singleton
            };
            if (addToList) RegisterContext(result);
            return result;
        }
        public TypeResolverTypeMappingContext RegisterType(Type type, ILazyValue lazyInstance, object[] options, bool addToList = true)
        {
            var fullName = type?.FullName;
            var result = new TypeResolverTypeMappingContext(fullName, null, options)
            {
                ToType = type,
                ToName = fullName,

                LazyInstance = lazyInstance,

                MappingType = MappingType.Singleton
            };
            if (addToList) RegisterContext(result);
            return result;
        }

        public void RegisterApplier(Type type, ITypeResolverApplier applier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (applier == null) throw new ArgumentNullException(nameof(applier));

            var context = GetContext(type);
            if (context == null)
                throw new InvalidOperationException(
                    LocalizationServices.Format<ITypeResolverLocalizationModel, DefaultTypeResolverLocalizationModel>(
                        x => x.TypeDescriptionNotFound, type.Name)
                    );

            var applierCallbacks = context.ApplierCallbacks;
            if (applierCallbacks == null)
            {
                applierCallbacks = new List<ITypeResolverApplier>();
                context.ApplierCallbacks = applierCallbacks;
            }

            applierCallbacks.Add(applier);
        }
    }
}