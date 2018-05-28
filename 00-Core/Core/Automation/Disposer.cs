using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Trace;

namespace AMKsGear.Core.Automation
{
    public interface IDisposer : IDisposable
    {
        void Enqueue(object disposable, IDisposeHandler handler);
        void Enqueue(IDisposable disposable, bool state);
        void Enqueue(object disposable, IDisposeHandler handler, bool state);
        void Enqueue(IDisposable first, params IDisposable[] disposables);
        void Dequeue(object disposable);
        void SetState(object disposable, bool state);
        bool? GetState(object disposable);
    }

    public class Disposer : IDisposer, IDisposable
    {
        protected class DisposeTrackingContext
        {
            public readonly object Disposable;
            public volatile bool IsDisposed;
            public volatile bool State;
            public readonly IDisposeHandler Handler;

            public DisposeTrackingContext(object disposable, IDisposeHandler handler, bool state)
            {
                Disposable = disposable;
                IsDisposed = true;
                State = state;
                Handler = handler ?? DefaultDisposeHandler.Instance;
            }
            public DisposeTrackingContext(object disposable, bool state)
            {
                Disposable = disposable;
                IsDisposed = true;
                State = state;
                Handler = DefaultDisposeHandler.Instance;
            }
        }

        protected readonly IDictionary<int, DisposeTrackingContext> Disposables;

        public bool ClearAfterDisposition { get; set; }

        private readonly bool _disposeOnDestructor = false;

        public Disposer()
        {
            Disposables = new Dictionary<int, DisposeTrackingContext>();
        }
        public Disposer(IEnumerable<IDisposable> disposables)
        {
            var dict = new Dictionary<int, DisposeTrackingContext>();

            foreach (var row in disposables)
            {
                if (row == null) throw new InvalidOperationException();
                dict.Add(RuntimeHelpers.GetHashCode(row), new DisposeTrackingContext(row, true));
            }

            Disposables = dict;
        }
        public Disposer(bool disposeOnDestruction, bool critical)
        {
            _disposeOnDestructor = disposeOnDestruction;
            Disposables = critical
                ? (IDictionary<int, DisposeTrackingContext>)new ConcurrentDictionary<int, DisposeTrackingContext>()
                : new Dictionary<int, DisposeTrackingContext>();
        }
        public Disposer(IEnumerable<IDisposable> disposables, bool critical)
        {
            var all = disposables.AsList();
            var converter = (Func<object, KeyValuePair<int, DisposeTrackingContext>>)(x =>
            {
                if (x == null) throw new InvalidOperationException();
                return new KeyValuePair<int, DisposeTrackingContext>(RuntimeHelpers.GetHashCode(x), new DisposeTrackingContext(x, true));
            });

            IDictionary<int, DisposeTrackingContext> dict;
            if (critical)
            {
                dict = new ConcurrentDictionary<int, DisposeTrackingContext>(all.Select(converter).ToList());
            }
            else
            {
                dict = new Dictionary<int, DisposeTrackingContext>(all.Count);
                foreach (var disp in all)
                {
                    dict.Add(converter(disp));
                }
            }
            
            Disposables = dict;
        }
        ~Disposer()
        {
            if (_disposeOnDestructor)
            {
                Dispose();
            }
        }

        public void Enqueue(object disposable, IDisposeHandler handler) => Enqueue(disposable, handler, true);
        public void Enqueue(IDisposable disposable, bool state) => Enqueue(disposable, null, state);
        public void Enqueue(object disposable, IDisposeHandler handler, bool state)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));

            if (handler == null && (disposable as IDisposable) == null)
            {
                //Logger.
                throw new InvalidOperationException();
            }

            Disposables.Add(RuntimeHelpers.GetHashCode(disposable), new DisposeTrackingContext(disposable, handler, state));
        }
        public void Enqueue(IDisposable first, params IDisposable[] disposables)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            //if (disposables == null) throw new ArgumentNullException(nameof(disposables));

            Disposables.Add(RuntimeHelpers.GetHashCode(first), new DisposeTrackingContext(first, null, true));

            if (disposables != null)
            {
                foreach (var disposable in disposables)
                {
                    Disposables.Add(RuntimeHelpers.GetHashCode(disposable),
                        new DisposeTrackingContext(disposable, null, true));
                }
            }
        }

        public void Dequeue(object disposable)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));

            var key = RuntimeHelpers.GetHashCode(disposable);

            if (Disposables.ContainsKey(key))
            {
                Disposables.Remove(key);
            }
        }

        public void SetState(object disposable, bool state)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));

            var key = RuntimeHelpers.GetHashCode(disposable);

            DisposeTrackingContext disp;
            if (Disposables.TryGetValue(key, out disp))
            {
                disp.State = state;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public bool? GetState(object disposable)
        {
            if (disposable == null) throw new ArgumentNullException(nameof(disposable));

            var key = RuntimeHelpers.GetHashCode(disposable);

            DisposeTrackingContext disp;
            if (Disposables.TryGetValue(key, out disp))
            {
                return disp.State;
            }

            return null;
        }

        public void Dispose()
        {
            _dispose();
        }
        public Task DisposeAsync() => Task.Run(() => _dispose());

        private void _dispose()
        {
            var disposables = Disposables.ToList();
            if (ClearAfterDisposition)
            {
                Disposables.Clear();
            }

            foreach (var disposableKv in disposables)
            {
                var disposable = disposableKv.Value;
                var disp = disposable.Disposable;
                if (disp != null && !disposable.IsDisposed && disposable.State)
                {
                    if (!disposable.IsDisposed && disposable.State)
                    {
                        disposable.IsDisposed = true;
                        disposable.Handler.DisposeObject(disp);
                    }
                }
            }
        }
    }
}
