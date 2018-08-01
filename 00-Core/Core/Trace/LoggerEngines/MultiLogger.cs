using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class MultiLogger : ILogChannel
    {
        public ILogChannel[] Loggers { get; }
        public bool DisposeLoggersOnDispose { get; }

        private object _lock = new object();

        public MultiLogger(IEnumerable<ILogChannel> loggers, bool disposeLoggersOnDispose)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            Loggers = loggers.ToArray();
            DisposeLoggersOnDispose = disposeLoggersOnDispose;
        }


        public void Dispose()
        {
            if (!DisposeLoggersOnDispose) return;

            lock (_lock)
            {
                foreach (var logger in Loggers)
                    logger.Dispose();
            }
        }

        public void LogString(
            string @string,
            ILoggingContext context,
            string callerMemberName = null,
            int callerLineNumber = 0,
            string callerFilePath = null)
        {
            lock (_lock)
            {
                foreach (var logger in Loggers)
                    logger.LogString(@string, context, callerMemberName, callerLineNumber, callerFilePath);
            }
        }

        public void LogException(
            Exception exception,
            ILoggingContext context,
            string callerMemberName = null,
            int callerLineNumber = 0,
            string callerFilePath = null)
        {
            lock (_lock)
            {
                foreach (var logger in Loggers)
                    logger.LogException(exception, context, callerMemberName, callerLineNumber, callerFilePath);
            }
        }
    }
}