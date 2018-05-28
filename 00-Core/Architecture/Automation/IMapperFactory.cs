namespace AMKsGear.Architecture.Automation
{
    public interface IMapperFactory
    {
        IMapper CreateInstance(object config = null);
    }
}