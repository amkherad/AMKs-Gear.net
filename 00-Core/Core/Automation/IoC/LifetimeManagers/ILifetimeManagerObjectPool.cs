using System.Collections.Generic;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public interface ILifetimeManagerObjectPool
    {
        void AddObject(ObjectTrackingInfo objectTrackingInfo);
        void RemoveObject(object @object);

        IEnumerable<ObjectTrackingInfo> GetTrackingInfos();
    }
}