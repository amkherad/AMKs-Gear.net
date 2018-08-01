using System.Linq.Expressions;

namespace AMKsGear.Architecture.Modeling
{
    /// <summary>
    /// An expression path to a member.
    /// </summary>
    public interface IModelMemberPath
    {
        Expression Path { get; }
    }
}