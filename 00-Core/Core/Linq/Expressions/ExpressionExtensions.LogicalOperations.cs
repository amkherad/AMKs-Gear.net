using System;
using System.Linq;
using System.Linq.Expressions;

namespace AMKsGear.Core.Linq.Expressions
{
    public static partial class ExpressionExtensions
    {
        #region Composer
        public static Expression<T> Compose<T>(this
            Expression<T> expression,
            Expression<T> appendix,
            Func<Expression, Expression, Expression> merge)
        {
            var parameters = expression.Parameters;
            var secondParameters = appendix.Parameters;

            // build parameter map (from parameters of second to parameters of first)
            var map = parameters.Select((f, i) => new { f, s = secondParameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ExpressionParameterRebinder.ReplaceParameters(map, appendix.Body);

            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(expression.Body, secondBody), parameters);
        }
        #endregion

        #region And
        public static Expression<Func<TResult>> And<TResult>(this
            Expression<Func<TResult>> expression,
            Expression<Func<TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TResult>> And<TArg1, TResult>(this
            Expression<Func<TArg1, TResult>> expression,
            Expression<Func<TArg1, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TResult>> And<TArg1, TArg2, TResult>(this
            Expression<Func<TArg1, TArg2, TResult>> expression,
            Expression<Func<TArg1, TArg2, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TResult>> And<TArg1, TArg2, TArg3, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> And<TArg1, TArg2, TArg3, TArg4, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> And<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> And<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> And<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> And<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> appendix)
        { return expression.Compose(appendix, Expression.And); }
        #endregion

        #region Or
        public static Expression<Func<TResult>> Or<TResult>(this
            Expression<Func<TResult>> expression,
            Expression<Func<TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TResult>> Or<TArg1, TResult>(this
            Expression<Func<TArg1, TResult>> expression,
            Expression<Func<TArg1, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TResult>> Or<TArg1, TArg2, TResult>(this
            Expression<Func<TArg1, TArg2, TResult>> expression,
            Expression<Func<TArg1, TArg2, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TResult>> Or<TArg1, TArg2, TArg3, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> Or<TArg1, TArg2, TArg3, TArg4, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> Or<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> Or<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> Or<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> Or<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> appendix)
        { return expression.Compose(appendix, Expression.Or); }
        #endregion

        #region AndAlso
        public static Expression<Func<TResult>> AndAlso<TResult>(this
            Expression<Func<TResult>> expression,
            Expression<Func<TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TResult>> AndAlso<TArg1, TResult>(this
            Expression<Func<TArg1, TResult>> expression,
            Expression<Func<TArg1, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TResult>> AndAlso<TArg1, TArg2, TResult>(this
            Expression<Func<TArg1, TArg2, TResult>> expression,
            Expression<Func<TArg1, TArg2, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TResult>> AndAlso<TArg1, TArg2, TArg3, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> AndAlso<TArg1, TArg2, TArg3, TArg4, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> AndAlso<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> AndAlso<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> AndAlso<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> AndAlso<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        #endregion

        #region OrElse
        public static Expression<Func<TResult>> OrElse<TResult>(this
            Expression<Func<TResult>> expression,
            Expression<Func<TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TResult>> OrElse<TArg1, TResult>(this
            Expression<Func<TArg1, TResult>> expression,
            Expression<Func<TArg1, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TResult>> OrElse<TArg1, TArg2, TResult>(this
            Expression<Func<TArg1, TArg2, TResult>> expression,
            Expression<Func<TArg1, TArg2, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TResult>> OrElse<TArg1, TArg2, TArg3, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> OrElse<TArg1, TArg2, TArg3, TArg4, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> OrElse<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> OrElse<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> OrElse<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> OrElse<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(this
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> expression,
            Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> appendix)
        { return expression.Compose(appendix, Expression.AndAlso); }
        #endregion
    }
}