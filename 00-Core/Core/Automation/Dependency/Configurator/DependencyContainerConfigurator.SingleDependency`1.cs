namespace AMKsGear.Core.Automation.Dependency.Configurator
{
    public partial class DependencyContainerConfigurator
    {
        public class SingleDependency<TBase> : DependencyKindBase, IDependencyKind
        {
            public TupleDependency<TBase, TDerived> To<TDerived>()
                where TDerived : TBase
            {
                return new TupleDependency<TBase, TDerived>();
            }
        }
    }
}