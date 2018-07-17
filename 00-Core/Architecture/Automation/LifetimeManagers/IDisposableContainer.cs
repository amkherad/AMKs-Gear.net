using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Architecture.Automation.LifetimeManagers
{
    /// <summary>
    /// Keep track of a list of disposable objects.
    /// </summary>
    public interface IDisposableContainer : ICollection, IEnumerable, IDisposable
    {
        /// <summary>
        /// Enqueues a single disposable object.
        /// </summary>
        /// <param name="disposable">The disposable object.</param>
        void Enqueue(IDisposable disposable);
        
        /// <summary>
        /// Enqueues a list of disposable objects.
        /// </summary>
        /// <param name="disposables">A list of disposable objects.</param>
        void Enqueue(IEnumerable<IDisposable> disposables);
        
        /// <summary>
        /// Removes an object.
        /// </summary>
        /// <param name="disposable">The disposable object to remove.</param>
        /// <returns>A boolean determining whether object successfully removed or not.</returns>
        bool Dequeue(IDisposable disposable);
    }
}