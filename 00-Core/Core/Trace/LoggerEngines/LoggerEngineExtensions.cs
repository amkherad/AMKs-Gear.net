using System;
using System.Collections.Generic;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public static class LoggerEngineExtensions
    {
        public static MultiLogger AttachLoggerEngine(this ILoggerEngine logger,
            ILoggerEngine attachment, bool disposeLoggersOnDispose)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            
            return new MultiLogger(new[]
            {
                logger, attachment
            }, disposeLoggersOnDispose);
        }
        public static MultiLogger AttachLoggerEngine(this ILoggerEngine logger,
            IEnumerable<ILoggerEngine> attachments, bool disposeLoggersOnDispose)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            return
                new MultiLogger(attachments.Merge(logger, RelativeOrder.Before), disposeLoggersOnDispose);
        }
    }
}