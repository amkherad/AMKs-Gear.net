using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.Linq.Expressions
{
    public static class ExpressionHelper
    {
        private const string ToLowerMethodName = nameof(String.ToLower);
        private const string ToUpperMethodName = nameof(String.ToUpper);
        private const string ContainsMethodName = nameof(String.Contains);
        private const string StartsWithMethodName = nameof(String.StartsWith);
        private const string EndsWithMethodName = nameof(String.EndsWith);

        #region Helpers
        public static Expression<Func<T, bool>> CreateCompareCondition<T>(string propertyName, T value, Compare compare)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "str");
            var property = type.GetRuntimeProperty(propertyName);
            var comparison = ExpressionHelper.GetCompareExpression(
                Expression.MakeMemberAccess(arg, property),
                Expression.Constant(value),
                compare);
            return Expression.Lambda<Func<T, bool>>(comparison, arg);
        }
        #endregion

        #region Builders
        //private static MethodInfo miTL = typeof(String).GetMethod("ToLower", Type.EmptyTypes);
        //private static MethodInfo miS = typeof(String).GetMethod("StartsWith", new Type[] { typeof(String) });
        //private static MethodInfo miC = typeof(String).GetMethod("Contains", new Type[] { typeof(String) });
        //private static MethodInfo miE = typeof(String).GetMethod("EndsWith", new Type[] { typeof(String) });

        public static BinaryExpression CreatePredicatedCondition<TArg, TResult>(PropertyInfo property, MethodInfo predicate, ParameterExpression parameter)
        {
            //var type = typeof (T);
            //var member = Expression.MakeMemberAccess(parameter, property);
            //var callExpression = ;

            //return Expression.Lambda<Func<TArg, TResult>>(callExpression, new[] {parameter});
            return null;
        }

        public static MethodCallExpression GetToLowerExpression(ParameterExpression parameter)
        { return Expression.Call(parameter, typeof(string).GetTypeInfo().GetDeclaredMethod(ToLowerMethodName)); }
        public static MethodCallExpression GetToUpperExpression(ParameterExpression parameter)
        { return Expression.Call(parameter, typeof(string).GetTypeInfo().GetDeclaredMethod(ToUpperMethodName)); }
        public static MethodCallExpression GetContainsExpression(Expression source, Expression find)
        {
            return Expression.Call(source, typeof(string).GetTypeInfo().GetDeclaredMethods(ContainsMethodName).First(
                x =>
                {
                    var parameters = x.GetParameters();
                    if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string)) return false;
                    return true;
                }), find);
        }
        public static UnaryExpression GetNotContainsExpression(Expression source, Expression find)
        { return Expression.Not(GetContainsExpression(source, find)); }
        public static MethodCallExpression GetStartsWithExpression(Expression source, Expression find)
        {
            return Expression.Call(source, typeof(string).GetTypeInfo().GetDeclaredMethods(ContainsMethodName).First(
                  x =>
                  {
                      var parameters = x.GetParameters();
                      if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string)) return false;
                      return true;
                  }), find);
        }
        public static UnaryExpression GetNotStartsWithExpression(Expression source, Expression find)
        { return Expression.Not(GetStartsWithExpression(source, find)); }
        public static MethodCallExpression GetEndsWithExpression(Expression source, Expression find)
        {
            return Expression.Call(source, typeof(string).GetTypeInfo().GetDeclaredMethods(ContainsMethodName).First(
                  x =>
                  {
                      var parameters = x.GetParameters();
                      if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string)) return false;
                      return true;
                  }), find);
        }
        public static UnaryExpression GetNotEndsWithExpression(Expression source, Expression find) => Expression.Not(GetEndsWithExpression(source, find));

        public static MethodCallExpression GetStringComparerEquals(Expression source, Expression find)
        {
            return Expression.Call(source, typeof(string).GetTypeInfo().GetDeclaredMethods(ContainsMethodName).First(
                  x =>
                  {
                      var parameters = x.GetParameters();
                      if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string)) return false;
                      return true;
                  }), find);
        }

        public static BinaryExpression GetCompareExpression(Expression parameter, Expression operand, Compare compare)
        {
            switch (compare)
            {
                case Compare.Equal:
                    return Expression.Equal(parameter, operand);
                case Compare.Lesser:
                    return Expression.LessThan(parameter, operand);
                case Compare.Greater:
                    return Expression.GreaterThan(parameter, operand);
                case Compare.LesserEqual:
                    return Expression.LessThanOrEqual(parameter, operand);
                case Compare.GreaterEqual:
                    return Expression.GreaterThanOrEqual(parameter, operand);
                case Compare.NotEqual:
                    return Expression.NotEqual(parameter, operand);
                case Compare.Near:
                    return Expression.Equal(parameter, operand);
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }

        public static Expression<Func<TEntity, bool>> GetInstanceStringCompareExpression<TEntity>(Expression parameter, Expression operand, ParameterExpression param,
            StringCompare compare, StringComparer comparer)
        {
            switch (compare)
            {
                case StringCompare.Equals:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(parameter, operand), param)
                        : null;
                case StringCompare.NotEquals:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(Expression.NotEqual(parameter, operand), param)
                        : null;
                case StringCompare.Contains:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetContainsExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotContains:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetNotContainsExpression(parameter, operand), param)
                        : null;
                case StringCompare.RegexLike:
                    return null;
                case StringCompare.StartsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetStartsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotStartsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetNotStartsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.EndsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetEndsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotEndsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<TEntity, bool>>(GetNotEndsWithExpression(parameter, operand), param)
                        : null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }
        public static Expression<Func<string, bool>> GetStringCompareExpression(Expression parameter, Expression operand, ParameterExpression param,
            StringCompare compare, StringComparer comparer)
        {
            switch (compare)
            {
                case StringCompare.Equals:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(Expression.Equal(parameter, operand), param)
                        : null;
                case StringCompare.NotEquals:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(Expression.NotEqual(parameter, operand), param)
                        : null;
                case StringCompare.Contains:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetContainsExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotContains:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetNotContainsExpression(parameter, operand), param)
                        : null;
                case StringCompare.RegexLike:
                    return null;
                case StringCompare.StartsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetStartsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotStartsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetNotStartsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.EndsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetEndsWithExpression(parameter, operand), param)
                        : null;
                case StringCompare.NotEndsWith:
                    return comparer == null
                        ? Expression.Lambda<Func<string, bool>>(GetNotEndsWithExpression(parameter, operand), param)
                        : null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }
        #endregion
    }
}