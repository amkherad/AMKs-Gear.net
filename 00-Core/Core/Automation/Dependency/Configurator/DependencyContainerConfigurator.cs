using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator : IConfigurator
    {
        public DependencyContainerContext Context { get; }
        
        private List<IDependencyKind> _dependencies;

        public bool AutoRegisterUnknownDependencies { get; set; }
        
        
        public DependencyContainerConfigurator(DependencyContainer dependencyContainer)
            : this(dependencyContainer.Context)
        {
        }

        public DependencyContainerConfigurator(DependencyContainerContext dependencyContainerContext)
        {
            Context = dependencyContainerContext ?? throw new ArgumentNullException(nameof(dependencyContainerContext));
            _dependencies = new List<IDependencyKind>();
        }


        public DependencyContainerConfigurator RegisterMap(IDictionary<Type, Type> mapping)
        {
            
            return this;
        }


        public DependencyContainerConfigurator RegisterStaticValue<T>(T value)
        {

            return this;
        }

        public DependencyContainerConfigurator RegisterStaticValue(Type type, object value)
        {

            return this;
        }

        public DependencyContainerConfigurator RegisterStaticValue(object value)
        {

            return this;
        }


        public SingleDependency<TBase> Add<TBase>()
        {
            
            return new SingleDependency<TBase>();
        }
        
        public TupleDependency<TBase, TDerived> Add<TBase, TDerived>()
            where TDerived : TBase
        {
            
            return new TupleDependency<TBase, TDerived>();
        }
        
        
        public void Dispose()
        {
        }

        public bool Validate()
        {
            return false;
        }

        public object GetUnderlyingContext() => Context;
    }
}