using System;
using AMKsGear.Architecture.Automation.Dependency;
using AMKsGear.Core.Automation.Dependency.Configurator;

namespace AMKsGear.Core.Automation.Dependency
{
    public static class DependencyContainerExtensions
    {
        public static DependencyContainerConfigurator Config(this IDependencyResolver dependencyResolver) =>
            new DependencyContainerConfigurator(
                dependencyResolver as DependencyContainer ?? throw new InvalidOperationException());

        public static DependencyContainerConfigurator Config(this DependencyContainer dependencyResolver) =>
            new DependencyContainerConfigurator(dependencyResolver);

        public static DependencyContainerConfigurator Config(this DependencyContainerContext context) =>
            new DependencyContainerConfigurator(context);
    }
}