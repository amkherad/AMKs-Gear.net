using System;
using AMKsGear.Core.Automation.IoC.Options;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers.Lifetimes
{
    public class ExternallyControlledLifetimeManager : TypeResolverOption, IIoCLifetimeManager, IDisposable
    {
        public object Instance { get; private set; }

        public void AddObjectTrackInLifetimeManager(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {
            Instance = instance;
        }

        public void ReleaseObject(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {

        }

        public void Dispose()
        {
            var disposable = Instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}