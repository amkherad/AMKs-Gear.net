using System;

namespace AMKsGear.Architecture.Trace
{
    public interface ILoggerEngine : IDisposable
    {
        void Feed(int repeat);
        void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath);
        void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath);
    }
}