using System.Linq.Expressions;
using System.Threading;
using AMKsGear.Architecture.Linq.Expressions;

namespace AMKsGear.Core.Linq.Expressions
{
    /// <inheritdoc />
    public class InternalExpressionCompiler : IExpressionCompiler
    {
        private static InternalExpressionCompiler _singleInstance;

        /// <summary>
        /// Single instance to 
        /// </summary>
        public static InternalExpressionCompiler Instance => LazyInitializer.EnsureInitialized(ref _singleInstance);

        
        
        protected InternalExpressionCompiler()
        {
        }
        
        /// <inheritdoc />
        public TFunc Compile<TFunc>(Expression<TFunc> source)
        {
            return source.Compile();
        }
    }
}