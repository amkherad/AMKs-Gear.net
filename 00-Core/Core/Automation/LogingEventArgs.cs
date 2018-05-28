using System;

namespace AMKsGear.Core.Automation
{
    public delegate void LogingEventHandler(object sender, LogingEventArgs eventArgs);

    public class LogingEventArgs : EventArgs
    {
        public string LogString { get; private set; }
        public object Tag { get; set; }
        public LogingEventArgs(string logString, object tag = null)
        {
            LogString = logString;
            Tag = tag;
        }
    }
}