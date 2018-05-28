using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data
{
    public interface IEntity : IModel
    {
        bool IsEvaluated { get; }
    }
}