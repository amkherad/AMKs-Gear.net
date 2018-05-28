using System;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.Linq.Expressions.StringExpressions
{
    public static class StringExpression
    {
        public const string StringExpressionParameterName = "stringParameter";

        public static Expression<Func<TEntity, bool>> GetInstanceStringCompareExpression<TEntity>(
            string propertyName, string value, StringCompare compare, StringComparer comparer)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, StringExpressionParameterName);
            var property = type.GetRuntimeProperty(propertyName);
            if(parameter == null) throw new InvalidOperationException($"Property '{propertyName}' not found.");
            var comparison = ExpressionHelper.GetInstanceStringCompareExpression<TEntity>(
                Expression.MakeMemberAccess(parameter, property),
                Expression.Constant(value),
                parameter,
                compare, comparer);
            return comparison;
        }
        public static Expression<Func<string, bool>> GetStringCompareExpression(string value, StringCompare compare, StringComparer comparer)
        {
            var type = typeof(string);
            var parameter = Expression.Parameter(type, StringExpressionParameterName);
            var constant = Expression.Constant(value);
            //var property = type.GetProperty(propertyName);
            return ExpressionHelper.GetStringCompareExpression(
                parameter,
                constant,
                parameter,
                compare,
                comparer);
        }
    }
}