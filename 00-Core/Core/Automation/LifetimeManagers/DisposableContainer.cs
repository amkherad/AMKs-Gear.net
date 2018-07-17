using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Automation.LifetimeManagers;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Automation.LifetimeManagers
{
    public class DisposableContainer : IDisposableContainer, IDisposable
    {
        protected readonly ICollection<IDisposable> Disposables;

        public bool ClearAfterDisposition { get; set; }

        public DisposableContainer()
        {
            Disposables = new List<IDisposable>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposables"></param>
        public DisposableContainer(IEnumerable<IDisposable> disposables)
        {
            Disposables = new List<IDisposable>(disposables);
        }

        public IEnumerator GetEnumerator() => Disposables.GetEnumerator();


        public void CopyTo(Array array, int index)
            => (Disposables as ICollection).CopyTo(array, index);

        public int Count => Disposables.Count;
        public bool IsSynchronized => (Disposables as ICollection).IsSynchronized;
        public object SyncRoot => (Disposables as ICollection).SyncRoot;
        
        public void Enqueue(IDisposable disposable) => Disposables.Add(disposable);
        public void Enqueue(IEnumerable<IDisposable> disposables) => Disposables.AddRange(disposables);

        public bool Dequeue(IDisposable disposable) => Disposables.Remove(disposable);

        public void Clear() => Disposables.Clear();

        public void Dispose()
        {
            var disposables = Disposables.ToList();
            if (ClearAfterDisposition)
            {
                Disposables.Clear();
            }

            foreach (var disposable in disposables)
            {
                disposable?.Dispose();
            }
        }
    }
}
