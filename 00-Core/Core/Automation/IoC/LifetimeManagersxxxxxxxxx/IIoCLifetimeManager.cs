namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public interface IIoCLifetimeManager// : IDisposable
    {
        void AddObjectTrackInLifetimeManager(ILifetimeManagedTypeResolver lifetimedResolver, object instance);

        void ReleaseObject(ILifetimeManagedTypeResolver lifetimedResolver, object instance);
    }
}