using System;

namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator
    {
        public class TupleDependency : DependencyKindBase, IDependencyKind
        {
            public Type BaseType { get; }
            public Type DerivedType { get; }


            public TupleDependency(Type baseType, Type derivedType)
            {
                BaseType = baseType;
                DerivedType = derivedType;
            }
        }
    }
}