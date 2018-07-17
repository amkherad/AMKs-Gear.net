using System.Linq.Expressions;

namespace AMKsGear.Architecture.Linq.Expressions
{
    public interface IExpressionCompiler
    {
        TFunc Compile<TFunc>(Expression<TFunc> source);
    }
}