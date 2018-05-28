using System;

namespace AMKsGear.Core.Automation
{
    public class GeneralStateEventArgs<TState> : EventArgs
    {
        public TState State { get; }

        public GeneralStateEventArgs(TState state)
        {
            State = state;
        }
    }
}