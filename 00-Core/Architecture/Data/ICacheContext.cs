using System;

namespace AMKsGear.Architecture.Data
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

        /// <summary>
        /// Enlarges the underlying list to given capacity.
        /// </summary>
        /// <param name="capacity"></param>
        void EnsureCapacity(int capacity);
    }
}