using System;
using AMKsGear.Core.Automation.IoC.Options;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers.Lifetimes
{
    public class ContainerControlledLifetimeManager : TypeResolverOption, IIoCLifetimeManager
    {
        public void AddObjectTrackInLifetimeManager(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {
            if (lifetimedResolver == null) throw new ArgumentNullException(nameof(lifetimedResolver));
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            lifetimedResolver.TrackObject(new ObjectTrackingInfo(lifetimedResolver, instance, this));
        }

        public void ReleaseObject(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {
            if (lifetimedResolver == null) throw new ArgumentNullException(nameof(lifetimedResolver));
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            lifetimedResolver.ReleaseObject(instance);
        }
    }
}