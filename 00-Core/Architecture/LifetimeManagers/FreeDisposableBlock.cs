using System;

namespace AMKsGear.Architecture.LifetimeManagers
{
    public class FreeDisposableBlock<TState> : IDisposable
    {
        protected readonly TState _state;

        public Action<TState> DisposeCallback { get; protected set; }
        public bool DisposeAtDestructor { get; protected set; }

        public FreeDisposableBlock(Action<TState> disposeCallback, TState state)
        {
            DisposeCallback = disposeCallback;
            _state = state;
            DisposeAtDestructor = false;
        }
        public FreeDisposableBlock(Action<TState> disposeCallback, TState state, bool disposeAtDestructor)
        {
            DisposeCallback = disposeCallback;
            _state = state;
            DisposeAtDestructor = disposeAtDestructor;
        }
        ~FreeDisposableBlock()
        {
            if (DisposeAtDestructor)
                Dispose();
        }

        public void Dispose()
        {
            DisposeCallback?.Invoke(_state);
        }
    }
    public class FreeDisposableBlock : FreeDisposableBlock<object>
    {
        public FreeDisposableBlock(Action<object> disposeCallback, object state) : base(disposeCallback, state) { }
        public FreeDisposableBlock(Action<object> disposeCallback, object state, bool disposeAtDestructor) : base(disposeCallback, state, disposeAtDestructor) { }
    }
}