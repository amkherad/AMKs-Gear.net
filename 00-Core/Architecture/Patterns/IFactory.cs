namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Provides basic access to factory pattern object. 
    /// </summary>
    /// <typeparam name="T">Target object type.</typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// Creates the object.
        /// </summary>
        /// <returns>The object.</returns>
        T Create();
    }
}