namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Provides sealing capability for a <see cref="IFluentConfigurator"/>
    /// </summary>
    public interface IFluentConfiguratorSealing : IFluentConfigurator
    {
        /// <summary>
        /// Seals the object and prevents more actions.
        /// </summary>
        void Seal();
        
//        /// <summary>
//        /// Checks whether the object can be sealed or not, if so seals the object.
//        /// </summary>
//        bool CheckAndSeal();
    }
}