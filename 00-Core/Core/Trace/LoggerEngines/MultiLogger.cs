using System;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class MultiLogger : ILoggerEngine
    {
        public ILoggerEngine[] Loggers { get; }
        public bool DisposeLoggersOnDispose { get; }

        public MultiLogger(IEnumerable<ILoggerEngine> loggers, bool disposeLoggersOnDispose)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            Loggers = loggers.ToArray();
            DisposeLoggersOnDispose = disposeLoggersOnDispose;
        }

        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            foreach (var logger in Loggers)
                logger.Write(@string, styles, context, callerMemberName, callerLineNumber, callerFilePath);
        }
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            foreach (var logger in Loggers)
                logger.Write(exception, styles, context);
        }
        public void Feed(int repeat)
        {
            foreach (var logger in Loggers)
                logger.Feed(repeat);
        }

        public void Dispose()
        {
            if (!DisposeLoggersOnDispose) return;
            foreach (var logger in Loggers)
                logger.Dispose();
        }
    }
}