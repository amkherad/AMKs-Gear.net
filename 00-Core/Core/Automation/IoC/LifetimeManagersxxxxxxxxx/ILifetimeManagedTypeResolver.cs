using System;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public interface ILifetimeManagedTypeResolver : ITypeResolver, IDisposable
    {
        void TrackObject(ObjectTrackingInfo objectTrackingInfo);
        void ReleaseObject(object @object);

        ILifetimeManagedTypeResolver Parent { get; }
    }
}