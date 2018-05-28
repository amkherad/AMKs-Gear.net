using System;

namespace AMKsGear.Core.Utils
{
    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs eventArgs);

    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}