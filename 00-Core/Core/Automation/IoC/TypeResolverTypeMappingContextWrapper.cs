using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC.LifetimeManagers;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Automation.IoC
{
    public class TypeResolverTypeMappingContextWrapper
    {
        private TypeResolverTypeMappingContext Context { get; }

        public TypeResolverTypeMappingContextWrapper(TypeResolverTypeMappingContext context)
        {
            Context = context;
        }

        public string StrongName => Context.BindingStrongName; //Context.StrongName;

        public string FromName => Context.FromName;
        public string ToName => Context.ToName;

        public Type FromType => Context.FromType;
        public Type ToType => Context.ToType;

        public Func<object> Factory1 => Context.Factory1;
        public Func<Type, object> Factory2 => Context.Factory2;
        public bool FactoryCache => Context.FactoryCache;

        public object Instance => Context.Instance;
        public ILazyValue LazyInstance => Context.LazyInstance;

        public long ResolveLifespan => Context.ResolveLifespan;
        public long ResolveCount => Context.ResolveCount;

        public MappingType MappingType => Context.MappingType;

        public List<ITypeResolverApplier> ApplierCallbacks => Context.ApplierCallbacks;

        public object[] Options => Context.Options;
        public IIoCLifetimeManager LifetimeManager => Context.LifetimeManager;
    }
}