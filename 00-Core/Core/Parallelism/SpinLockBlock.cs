using System.Threading;
using AMKsGear.Architecture.Parallelism;

namespace AMKsGear.Core.Parallelism
{
    /// <summary>
    /// Provides a <see cref="SpinLock"/> synchronization block.
    /// </summary>
    /// <remarks>
    /// Suitable for providing external access to a private lock.
    /// NOTE: SpinLock is a non-reentrant lock, meaning that if a thread holds the lock, it is not allowed to enter the lock again. attempting to enter a lock already held will result in deadlock.
    /// </remarks>
    public class SpinLockBlock : ISyncBlock
    {
        private SpinLock _lockTarget;
        
        private const int FALSE = 1;
        private const int TRUE = 0;
        
        private int _lockReleased; //0 : true | 1 : false

        
        public SpinLockBlock(SpinLock lockTarget)
        {
            _lockTarget = lockTarget;

            var lockTaken = false;
            _lockTarget.Enter(ref lockTaken);
            if (lockTaken)
            {
                _lockReleased = FALSE; // false
            }
        }
        
        public void Dispose()
        {
            if (Interlocked.Exchange(ref _lockReleased, TRUE) == FALSE)
            {
                _lockTarget.Exit();
            }
        }

        public object GetUnderlyingContext() => _lockTarget;
    }
}