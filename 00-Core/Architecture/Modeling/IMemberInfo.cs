using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    public interface IMemberInfo : IWrapper
    {
        string Name { get; }
    }
}