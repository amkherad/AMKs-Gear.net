using System;

namespace AMKsGear.Architecture.LifetimeManagers
{
    public class FreeActionBlock<TState> : IActionBlock
    {
        protected readonly TState _state;
        public TState State => _state;

        public bool Disposing { get; protected set; } = true;

        public Action<TState> DisposeCallback { get; protected set; }

        public bool DisposeAtDestructor { get; protected set; }

        public FreeActionBlock(Action<TState> disposeCallback, TState state)
        {
            DisposeCallback = disposeCallback;
            _state = state;
            DisposeAtDestructor = false;
        }
        public FreeActionBlock(Action<TState> disposeCallback, TState state, bool disposeAtDestructor)
        {
            DisposeCallback = disposeCallback;
            _state = state;
            DisposeAtDestructor = disposeAtDestructor;
        }
        ~FreeActionBlock()
        {
            if (DisposeAtDestructor)
                Dispose();
        }

        public void Dispose()
        {
            if (Disposing)
                DisposeCallback?.Invoke(_state);
        }

        public void Cancel()
        {
            Disposing = true;
        }
    }
    public class FreeActionBlock : FreeActionBlock<object>
    {
        public FreeActionBlock(Action<object> disposeCallback, object state) : base(disposeCallback, state) { }
        public FreeActionBlock(Action<object> disposeCallback, object state, bool disposeAtDestructor) : base(disposeCallback, state, disposeAtDestructor) { }
    }
}