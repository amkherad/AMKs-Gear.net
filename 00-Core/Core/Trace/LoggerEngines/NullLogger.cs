using System;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class NullLogger : ILogChannel
    {
        public void Dispose()
        {

        }

        public void LogString(string @string, ILoggingContext context, string callerMemberName = null, int callerLineNumber = 0,
            string callerFilePath = null)
        {
            
        }

        public void LogException(Exception exception, ILoggingContext context, string callerMemberName = null,
            int callerLineNumber = 0, string callerFilePath = null)
        {
            
        }
    }
}