using System.Linq.Expressions;

namespace AMKsGear.Architecture.Automation
{
    public interface IMemberPath
    {
        Expression Path { get; }
    }
}