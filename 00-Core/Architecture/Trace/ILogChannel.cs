using System;

namespace AMKsGear.Architecture.Trace
{
    /// <summary>
    /// Provides access to logger.
    /// </summary>
    public interface ILogChannel
    {
        /// <summary>
        /// Log a string.
        /// </summary>
        /// <param name="string"></param>
        /// <param name="context"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="callerFilePath"></param>
        void LogString(string @string, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath);
        
        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="callerFilePath"></param>
        void LogException(Exception exception, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath);
    }
}