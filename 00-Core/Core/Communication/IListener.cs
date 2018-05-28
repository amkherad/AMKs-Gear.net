using System;

namespace AMKsGear.Core.Communication
{
    public enum ListenerState
    {
        Stop,
        Listening,
        End,
        Error,
        Unknown
    }

    public interface IListener
    {
        void Start();
        void Stop();
        ListenerState State { get; }
    }
    public interface IListener<TEventArg> : IListener
        where TEventArg : EventArgs
    {
        event EventHandler<TEventArg> DataReceived;
    }
}