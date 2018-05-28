using System;
using System.Threading;
using AMKsGear.Architecture.Parallelism;

namespace AMKsGear.Core.Parallelism
{
    public class MonitorBlock : ISyncBlock
    {
        private readonly object _lockTarget;
        private int _lockReleased;

        public MonitorBlock(object lockTarget)
        {
            if (ReferenceEquals(lockTarget, null)) throw new ArgumentNullException(nameof(lockTarget));

            _lockTarget = lockTarget;

            Monitor.Enter(lockTarget);
        }
        
        public void Dispose()
        {
            if (Interlocked.Increment(ref _lockReleased) == 0)
            {
                Monitor.Exit(_lockTarget);
            }
        }
    }
}