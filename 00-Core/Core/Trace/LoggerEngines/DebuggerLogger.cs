using System;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class DebuggerLogger : ILogChannel
    {
        public void LogString(string @string, ILoggingContext context, string callerMemberName = null, int callerLineNumber = 0,
            string callerFilePath = null)
        {
            System.Diagnostics.Debug.WriteLine(@string);
        }

        public void LogException(Exception exception, ILoggingContext context, string callerMemberName = null,
            int callerLineNumber = 0, string callerFilePath = null)
        {
            System.Diagnostics.Debug.WriteLine(exception);
        }

        public void Dispose()
        {
        }
    }
}