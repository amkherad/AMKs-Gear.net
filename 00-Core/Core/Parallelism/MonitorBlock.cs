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
            _lockTarget = lockTarget ?? throw new ArgumentNullException(nameof(lockTarget));

            Monitor.Enter(lockTarget);
        }
        
        public void Dispose()
        {
            if (Interlocked.Increment(ref _lockReleased) == 0)
            {
                Monitor.Exit(_lockTarget);
            }
        }

        public object GetUnderlyingContext() => _lockTarget;
    }
}