using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Automation;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Automation
{
    public static class ValueResolverExtensions
    {
        public static object GetValue(this IValueResolver valueResolver,
            Expression<Func<object, object>> memberExpression)
            => valueResolver.GetValue(memberExpression.GetMemberName());
        public static TResult GetValue<T, TResult>(this IValueResolver<TResult> valueResolver,
            Expression<Func<T, object>> memberExpression)
            => valueResolver.GetValue(memberExpression.GetMemberName());
    }
}