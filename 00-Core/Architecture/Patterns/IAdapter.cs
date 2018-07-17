namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Provides access to underlying context of adapter pattern object.
    /// </summary>
    public interface IAdapter
    {
        /// <summary>
        /// Get access to underlying object.
        /// </summary>
        /// <returns>Objects underlying context.</returns>
        object GetUnderlyingContext();
    }
}