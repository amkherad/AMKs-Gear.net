using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data.Serialization.Configurator
{
    public abstract class SerializationConfiguratorBase : IConfigurator
    {
        public abstract object GetUnderlyingContext();

        public abstract void Dispose();

        public abstract bool Validate();
    }
}