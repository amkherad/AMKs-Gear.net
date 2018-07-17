using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    public interface IMemberInfo : IAdapter
    {
        string Name { get; }
    }
}