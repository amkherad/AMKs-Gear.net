using System;
using AMKsGear.Core.Automation.IoC.Options;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers.Lifetimes
{
    public class HierarchicalLifetimeManager : TypeResolverOption, IIoCLifetimeManager
    {
        public void AddObjectTrackInLifetimeManager(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {
            if (lifetimedResolver == null) throw new ArgumentNullException(nameof(lifetimedResolver));
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            var parent = lifetimedResolver.Parent;
            while (parent != null)
            {
                lifetimedResolver = parent;
                parent = lifetimedResolver.Parent;
            }

            lifetimedResolver.TrackObject(new ObjectTrackingInfo(lifetimedResolver, instance, this));
        }

        public void ReleaseObject(ILifetimeManagedTypeResolver lifetimedResolver, object instance)
        {
            if (lifetimedResolver == null) throw new ArgumentNullException(nameof(lifetimedResolver));
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            var parent = lifetimedResolver.Parent;
            while (parent != null)
            {
                lifetimedResolver = parent;
                parent = lifetimedResolver.Parent;
            }

            lifetimedResolver.ReleaseObject(instance);
        }
    }
}