using System.Linq.Expressions;

namespace AMKsGear.Architecture.Linq.Expressions
{
    /// <summary>
    /// Provides abstraction layer to expression compiler.
    /// </summary>
    public interface IExpressionCompiler
    {
        /// <summary>
        /// Compiles a simple <c>Expression&lt;TFunc&gt;</c>.
        /// </summary>
        /// <param name="source">The expression to compile to a clr function.</param>
        /// <typeparam name="TFunc">Type of the function.</typeparam>
        /// <returns>The compiled function.</returns>
        TFunc Compile<TFunc>(Expression<TFunc> source);
    }
}