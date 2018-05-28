using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Patterns.AppModel
{
    public interface ITypeResolverCrossCuttingContext : ICrossCuttingContext
    {
        ITypeResolver GetTypeResolver();
        void SetTypeResolver(ITypeResolver typeResolver);
    }
}