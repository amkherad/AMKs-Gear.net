using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Patterns.AppModel
{
    public interface ITypeResolverAppContext : IAppContext
    {
        ITypeResolver TypeResolver { get; }

        void SetTypeResolver(ITypeResolver typeResolver);
    }
}