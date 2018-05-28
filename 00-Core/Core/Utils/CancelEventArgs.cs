using System;

namespace AMKsGear.Core.Utils
{
    public delegate void CancelEventHandler(object sender, CancelEventArgs eventArgs);

    public class CancelEventArgs : EventArgs
    {
        public bool IsCanceled { get; private set; }

        public CancelEventArgs() { }
        public CancelEventArgs(bool state)
        {
            IsCanceled = state;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}

