using System;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class HierarchyLogger : ILoggerEngine
    {
        public Func<string, string> PathFormatter { get; } = s =>
        {
            return s;
        };

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
            //Write(new StringBuilder(repeat).Insert(0, Environment.NewLine, repeat).ToString(), string.Empty, null);
        }

        public void Dispose()
        {

        }
    }
}