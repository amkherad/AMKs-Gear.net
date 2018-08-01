using System.Collections.Generic;

namespace AMKsGear.Architecture.Trace
{
    /// <summary>
    /// Provides information about current application environment.
    /// </summary>
    public interface ILoggingContext
    {
        /// <summary>
        /// This will override the name of the platform currently running on.
        /// </summary>
        string PlatformName { get; }
        
        /// <summary>
        /// This will override the name of the app domain currently running on.
        /// </summary>
        string AppDomain { get; }
        
        /// <summary>
        /// This will override the name of the application.
        /// </summary>
        string Application { get; }
        
        /// <summary>
        /// Extended options for log channel.
        /// </summary>
        /// <remarks>
        /// For example if the log channel is console, it can change fore-color or typeface.
        /// </remarks>
        IDictionary<string, object> Options { get; }
    }
}