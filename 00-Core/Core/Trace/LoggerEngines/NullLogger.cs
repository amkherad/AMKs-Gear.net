using System;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class NullLogger : ILoggerEngine
    {
        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {

        }
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {

        }
        public void Feed(int repeat)
        {

        }

        public void Dispose()
        {

        }
    }
}