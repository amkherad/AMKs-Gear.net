using System;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Provides access to extended mapper functions.
    /// </summary>
    public interface IMapperEx : IMapper
    {
        /// <summary>
        /// Creates a function to map an object with type of <c>srcType</c> to an object with type of <see cref="destinationType"/>.
        /// </summary>
        /// <param name="destinationType">Type of the destination object.</param>
        /// <param name="sourceType">Type of the source object.</param>
        /// <param name="options">Extended mapper engine specific options.</param>
        /// <returns>A function with source as input and destination as output.</returns>
        Func<object, object> GetMapFunction(Type destinationType, Type sourceType, object[] options);
        
        /// <summary>
        /// Same as <c>GetMapFunction</c>; But it returns map function as an <c>Action</c>.
        /// </summary>
        /// <param name="destinationType">Type of the destination object.</param>
        /// <param name="sourceType">Type of the source object.</param>
        /// <param name="options">Extended mapper engine specific options.</param>
        /// <returns>An action to map source to destination with destination as first parameter and source as second parameter.</returns>
        Action<object, object> GetMapAction(Type destinationType, Type sourceType, object[] options);
        
        /// <summary>
        /// Creates a function to map an object with type of <c>TSource</c> to an object with type of <c>TDestination</c>.
        /// </summary>
        /// <param name="options">Extended mapper engine specific options.</param>
        /// <typeparam name="TDestination">Type of the destination object.</typeparam>
        /// <typeparam name="TSource">Type of the source object.</typeparam>
        /// <returns>A function with source as input and destination as output.</returns>
        Func<TSource, TDestination> GetMapFunction<TDestination, TSource>(object[] options);
        
        /// <summary>
        /// Same as <c>GetMapFunction</c>; But it returns map function as an <c>Action</c>.
        /// </summary>
        /// <param name="options">Extended mapper engine specific options.</param>
        /// <typeparam name="TDestination">Type of the destination object.</typeparam>
        /// <typeparam name="TSource">Type of the source object.</typeparam>
        /// <returns>An action to map source to destination with destination as first parameter and source as second parameter.</returns>
        Action<TDestination, TSource> GetMapAction<TDestination, TSource>(object[] options);
    }
}