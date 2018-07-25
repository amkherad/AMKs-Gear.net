using System;
using System.Linq.Expressions;

namespace AMKsGear.MSTests.Core.Linq
{
    public class ExpressionTestBase
    {
        public Expression<Func<TParam, TResult>> CreateExpression<TParam, TResult>(Expression body, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<Func<TParam, TResult>>(body, parameters);
        }
    }
}