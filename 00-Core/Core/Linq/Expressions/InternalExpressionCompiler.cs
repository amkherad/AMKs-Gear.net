using System.Linq.Expressions;
using System.Threading;
using AMKsGear.Architecture.Linq.Expressions;

namespace AMKsGear.Core.Linq.Expressions
{
    public class InternalExpressionCompiler : IExpressionCompiler
    {
        private static InternalExpressionCompiler _singleInstance;

        public static InternalExpressionCompiler Instance => LazyInitializer.EnsureInitialized(ref _singleInstance);
        
        

        public TFunc Compile<TFunc>(Expression<TFunc> source)
        {
            return source.Compile();
        }
    }
}