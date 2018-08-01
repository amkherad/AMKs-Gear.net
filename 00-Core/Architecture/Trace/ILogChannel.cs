using System;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Trace
{
    /// <summary>
    /// Provides access to logger.
    /// </summary>
    public interface ILogChannel : IDisposable
    {
        /// <summary>
        /// Log a string.
        /// </summary>
        /// <param name="string"></param>
        /// <param name="context"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="callerFilePath"></param>
        void LogString(string @string,
            [CanBeNull] ILoggingContext context,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null);
        
        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerLineNumber"></param>
        /// <param name="callerFilePath"></param>
        void LogException(Exception exception,
            [CanBeNull] ILoggingContext context,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null);
    }
}