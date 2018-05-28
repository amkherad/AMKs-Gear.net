using System;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class MethodLogger : ILoggerEngine
    {
        public Action<string> Writer { get; }
        public Action<int> Feeder { get; }

        public MethodLogger(Action<string> writer, Action<int> feeder)
        {
            Writer = writer;
            Feeder = feeder;
        }

        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath) => Writer?.Invoke(@string);
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath) => Writer?.Invoke(exception.Message);
        public void Feed(int repeat) => Feeder?.Invoke(repeat);
        
        public void Dispose()
        {

        }
    }
}