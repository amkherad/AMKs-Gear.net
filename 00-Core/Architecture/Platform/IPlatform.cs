using System;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Platform
{
    /// <summary>
    /// A mechanism to provide platform-specified information.
    /// </summary>
    public interface IPlatform : IAdapter
    {
        /// <summary>
        /// The platform which application is running on.
        /// </summary>
        PlatformType Type { get; }
        
        /// <summary>
        /// Name of the platform.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Optional description about platform.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Optional host of application.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Application entry object.
        /// </summary>
        Type EntryType { get; }
        /// <summary>
        /// Application entry if it's available.
        /// </summary>
        object EntryIfAvailable { get; }
    }
}