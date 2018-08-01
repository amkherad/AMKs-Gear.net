using AMKsGear.Architecture.Patterns;
using AMKsGear.Architecture.Platform;

namespace AMKsGear.Architecture.Automation
{
    public interface IAssemblyActivator
    {
        void Activate(IPlatform platform, IAppContext context);
    }
}