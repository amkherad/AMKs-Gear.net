using System;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator
    {
        public class SingleDependency : DependencyKindBase, IDependencyKind
        {
            public Type BaseType { get; }

            public SingleDependency(Type baseType)
            {
                BaseType = baseType ?? throw new ArgumentNullException(nameof(baseType));
            }

            public TupleDependency To([NotNull] Type derived)
            {
                return new TupleDependency(BaseType, derived);
            }
        }
    }
}