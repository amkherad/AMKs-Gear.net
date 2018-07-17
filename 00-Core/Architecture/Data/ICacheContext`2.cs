namespace AMKsGear.Architecture.Data
{
    /// <summary>
    /// Provides basic cache service functionality.
    /// </summary>
    /// <typeparam name="TContentDescriptor">The identifier to the unique content.</typeparam>
    /// <typeparam name="TContent">The type of the content which cache service used for.</typeparam>
    public interface ICacheContext<in TContentDescriptor, TContent> : ICacheContext
    {
        /// <summary>
        /// Returns the cached content only if exists otherwise return default value.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>The content or default(content).</returns>
        TContent GetOrDefault(TContentDescriptor descriptor);
        
        /// <summary>
        /// Returns the cached content if exists otherwise throws an exception.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>The content.</returns>
        TContent Get(TContentDescriptor descriptor);
        
        /// <summary>
        /// Check whether the content is cached or not, if so returns the context via out parameter.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <param name="content">The content if existed.</param>
        /// <returns>A boolean determining the existence of the content in the cache list</returns>
        bool TryGet(TContentDescriptor descriptor, out TContent content);
        
        /// <summary>
        /// Check whether the content is cached or not.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>A boolean determining the existence of the content in the cache list</returns>
        bool Exists(TContentDescriptor descriptor);
        
        /// <summary>
        /// Adds content to cache list.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <param name="content">The content to store with specified descriptor.</param>
        /// <returns>A boolean determining if the content is new or overwritten (true if already existed).</returns>
        bool Cache(TContentDescriptor descriptor, TContent content);
        
        /// <summary>
        /// Misses the cache entry.
        /// </summary>
        /// <param name="descriptor">The identifier to the unique content.</param>
        /// <returns>A boolean determining if the content already exists or not.</returns>
        bool Miss(TContentDescriptor descriptor);
    }
}