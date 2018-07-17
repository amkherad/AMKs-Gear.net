using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Automation.IoC.Options;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public class LifeTimeControlledTypeResolver : ILifetimeManagedTypeResolver
    {
        private readonly ITypeResolver _typeResolver;
        private readonly ILifetimeManagerObjectPool _objectPool;
        public ILifetimeManagedTypeResolver Parent { get; }

        public LifeTimeControlledTypeResolver()
        {
            Parent = null;
            _typeResolver = new TypeResolverContainer();
            _objectPool = new LifetimeManagerObjectPool();
        }
        public LifeTimeControlledTypeResolver(ILifetimeManagedTypeResolver parent)
        {
            Parent = parent;
            _typeResolver = new TypeResolverContainer();
            _objectPool = new LifetimeManagerObjectPool();
        }
        public LifeTimeControlledTypeResolver(ITypeResolver typeResolver)
        {
            Parent = null;
            _typeResolver = typeResolver;
            _objectPool = new LifetimeManagerObjectPool();
        }
        public LifeTimeControlledTypeResolver(ILifetimeManagedTypeResolver parent, ITypeResolver typeResolver)
        {
            Parent = parent;
            _typeResolver = typeResolver;
            _objectPool = new LifetimeManagerObjectPool();
        }

        public object GetUnderlyingContext() => _typeResolver;

        public object Resolve(Type type, object context, IEnumerable<object> args)
        {
            var query = new _Internal_IoCQueryContext();
            var instance = _typeResolver.Resolve(type, context, args.Merge(query));
            var mappingContext = query.Context;

            if (instance != null)
            {
                var lifetimeManager = mappingContext.LifetimeManager;
                if (lifetimeManager != null)
                {
                    lifetimeManager.AddObjectTrackInLifetimeManager(this, instance);
                }
                else
                {
                    this.TrackObject(new ObjectTrackingInfo(this, instance, null));
                }
            }

            return instance;
        }
        public bool CanResolve(Type type, object context, IEnumerable<object> args)
            => _typeResolver.CanResolve(type, context, args);

        public void Dispose()
        {
            var allInfos = _objectPool.GetTrackingInfos();
            foreach (var info in allInfos)
            {
                info.Dispose();
            }
        }

        public void TrackObject(ObjectTrackingInfo objectTrackingInfo)
        {
            if (objectTrackingInfo == null) throw new ArgumentNullException(nameof(objectTrackingInfo));
            _objectPool.AddObject(objectTrackingInfo);
        }

        public void ReleaseObject(object @object)
        {
            _objectPool.RemoveObject(@object);
        }
    }
}