using System;

namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Provides basic methods for every cache container.
    /// </summary>
    public interface ICacheContext : IDisposable
    {
        /// <summary>
        /// Clears the entire cache list.
        /// </summary>
        void Clear();
    }
}