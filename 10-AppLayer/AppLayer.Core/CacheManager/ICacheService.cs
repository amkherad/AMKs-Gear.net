using AMKsGear.Architecture.Patterns;

namespace AMKsGear.AppLayer.Core.CacheManager
{
    /// <summary>
    /// Provides basic cache service functionalities.
    /// </summary>
    /// <typeparam name="TContent">The type of the content which cache service used for.</typeparam>
    /// <typeparam name="TContentDescriptor">The identifier to the unique content.</typeparam>
    public interface ICacheService<TContent, in TContentDescriptor> : ICacheContext
    {
        /// <summary>
        /// Returns the cached content only if exists otherwise return default value.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns></returns>
        TContent GetOrDefault(TContentDescriptor descriptor);
        /// <summary>
        /// Returns the cached content if exists otherwise throws an exception.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns></returns>
        TContent Get(TContentDescriptor descriptor);
        
        /// <summary>
        /// Check whenever the content is cached or not.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>A boolean determining the existence of the content in cache list</returns>
        bool Exists(TContentDescriptor descriptor);
        
        /// <summary>
        /// Tries to load the content.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <param name="content">The content to store with specified descriptor.</param>
        /// <returns>A boolean determining if the content is new or overwritten.</returns>
        bool Cache(TContentDescriptor descriptor, TContent content);
        /// <summary>
        /// Misses the cache entry.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>A boolean determining if the content already exists or not.</returns>
        bool Miss(TContentDescriptor descriptor);
    }
}