﻿using System;
using System.Threading;
using AMKsGear.Architecture.Parallelism;

namespace AMKsGear.Core.Parallelism
{
    /// <summary>
    /// Provides a <see cref="Monitor"/> synchronization block.
    /// </summary>
    /// <remarks>
    /// Suitable for providing external access to a private lock.
    /// </remarks>
    public class MonitorBlock : ISyncBlock
    {
        private readonly object _lockTarget;

        private const int FALSE = 1;
        private const int TRUE = 0;
        
        private int _lockReleased; //0 : true | 1 : false

        public MonitorBlock(object lockTarget)
        {
            _lockTarget = lockTarget ?? throw new ArgumentNullException(nameof(lockTarget));

            var lockTaken = false;
            Monitor.Enter(lockTarget, ref lockTaken);
            if (lockTaken)
            {
                _lockReleased = FALSE; // false
            }
        }
        
        public void Dispose()
        {
            if (Interlocked.Exchange(ref _lockReleased, TRUE) == FALSE)
            {
                Monitor.Exit(_lockTarget);
            }
        }

        public object GetUnderlyingContext() => _lockTarget;
    }
}