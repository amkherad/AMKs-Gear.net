using System;
using System.Threading;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class MethodLogger : ILogChannel
    {
        public Action<string> Writer { get; }

        private SpinLock _lock;

        public MethodLogger(Action<string> writer)
        {
            Writer = writer;
            
            _lock = new SpinLock();
        }
        
        public void Dispose()
        {
            
        }

        public void LogString(string @string, ILoggingContext context, string callerMemberName = null, int callerLineNumber = 0,
            string callerFilePath = null)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                Writer(@string);
            }
            finally
            {
                if (lockTaken) _lock.Exit();
            }
        }

        public void LogException(Exception exception, ILoggingContext context, string callerMemberName = null,
            int callerLineNumber = 0, string callerFilePath = null)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                Writer(exception.ToString());
            }
            finally
            {
                if (lockTaken) _lock.Exit();
            }
        }
    }
}