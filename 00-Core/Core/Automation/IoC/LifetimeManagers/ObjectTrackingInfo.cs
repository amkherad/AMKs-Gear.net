using System;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public class ObjectTrackingInfo : IDisposable
    {
        public ILifetimeManagedTypeResolver TypeResolver { get; }

        public object Instance { get; }
        public IIoCLifetimeManager LifetimeManager { get; }

        public ObjectTrackingInfo(ILifetimeManagedTypeResolver typeResolver, object instance, IIoCLifetimeManager lifetimeManager)
        {
            TypeResolver = typeResolver;
            Instance = instance;
            LifetimeManager = lifetimeManager;
        }

        public void Dispose()
        {
            var instance = Instance;
            if (instance != null)
            {
                LifetimeManager?.ReleaseObject(TypeResolver, instance);
            }
        }
    }
}