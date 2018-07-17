using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Creates a customized mapper that offering high performance for cached objects.
    /// </summary>
    public interface IMapperBuilder : IFluentConfigurator, IFluentConfiguratorSealing
    {
        /// <summary>
        /// Compiles a mapper for high performance mapping.
        /// </summary>
        /// <returns>A compiled mapper.</returns>
        IMapper Compile();
        
        
    }
}