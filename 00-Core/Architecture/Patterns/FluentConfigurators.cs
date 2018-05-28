namespace AMKsGear.Architecture.Patterns
{
    public interface IFluentConfigurator
    {
       
    }
    public interface IFluentConfiguratorSealing : IFluentConfigurator
    {
        void Seal();
        void CheckAndSeal();
    }
}