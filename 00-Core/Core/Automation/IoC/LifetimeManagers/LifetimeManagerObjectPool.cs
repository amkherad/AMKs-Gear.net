using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC.LifetimeManagers
{
    public class LifetimeManagerObjectPool : ILifetimeManagerObjectPool, IWrapper, ICollection, IEnumerable
    {
        private readonly ICollection<ObjectTrackingInfo> _objectPool = new HashSet<ObjectTrackingInfo>();//<object>();

        public void AddObject(ObjectTrackingInfo objectTrackingInfo)
        {
            _objectPool.Add(objectTrackingInfo);
        }
        
        public void RemoveObject(object @object)
        {
            _objectPool.Remove(_objectPool.FirstOrDefault(x => x.Instance == @object));
        }

        public IEnumerable<ObjectTrackingInfo> GetTrackingInfos()
            => _objectPool;

        #region ICollection Implementations
        public IEnumerator GetEnumerator() => _objectPool.GetEnumerator();
        public void CopyTo(Array array, int index) => ((ICollection)_objectPool).CopyTo(array, index);
        public int Count => _objectPool.Count;
        public bool IsSynchronized => ((ICollection) _objectPool).IsSynchronized;
        public object SyncRoot => ((ICollection) _objectPool).SyncRoot;
        #endregion

        public object GetUnderlyingContext() => _objectPool;
    }
}