namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator
    {
        public class TupleDependency<TBase, TDerived> : DependencyKindBase, IDependencyKind
            where TDerived : TBase
        {
        }
    }
}