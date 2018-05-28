using System;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class DebuggerLogger : ILoggerEngine
    {
        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            System.Diagnostics.Debug.WriteLine(@string);
        }
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
        public void Feed(int repeat)
        {
            System.Diagnostics.Debug.WriteLine(new StringBuilder(repeat).Insert(0, Environment.NewLine, repeat).ToString());
        }

        public void Dispose()
        {

        }
    }
}