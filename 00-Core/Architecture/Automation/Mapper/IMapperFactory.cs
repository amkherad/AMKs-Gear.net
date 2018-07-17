namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// A factory class to create mapper instances.
    /// </summary>
    public interface IMapperFactory
    {
        /// <summary>
        /// Creates a new instance of the mapper using given config.
        /// </summary>
        /// <param name="config">A mapper engine specific configuration object.</param>
        /// <returns>An instance of <see cref="IMapper"/>.</returns>
        IMapper CreateDynamicMapper(object config = null);
    }
}