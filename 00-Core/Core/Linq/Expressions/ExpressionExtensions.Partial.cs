using System;
using System.Linq.Expressions;

namespace AMKsGear.Core.Linq.Expressions
{
    public static partial class ExpressionExtensions
    {
        public static string GetFunctionName(this MethodCallExpression expression) => expression?.Method.Name ?? string.Empty;

        #region Action<...>
        public static string GetMethodName(this Expression<Action> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg>(this Expression<Action<TArg>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2>(this Expression<Action<TArg1, TArg2>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3>(this Expression<Action<TArg1, TArg2, TArg3>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4>(this Expression<Action<TArg1, TArg2, TArg3, TArg4>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5>(this Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(this Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(this Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(this Expression<Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        #endregion
        #region Func<..., TResult>
        public static string GetMethodName<TResult>(this Expression<Func<TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg, TResult>(this Expression<Func<TArg, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TResult>(this Expression<Func<TArg1, TArg2, TResult>> expression){ return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TArg4, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        public static string GetMethodName<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(this Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> expression) { return (expression.Body as MethodCallExpression).GetFunctionName(); }
        #endregion
        #region GetMemberName
        public static string GetMemberName<T>(this Expression<Func<T, object>> expression)
            => GetMemberInfo(expression).Member.Name;
        public static string GetMemberName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
            => GetMemberInfo(expression).Member.Name;
        private static MemberExpression GetMemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null) throw new ArgumentNullException(nameof(method));

            MemberExpression memberExpr = null;

            var body = lambda.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)body).Operand as MemberExpression;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = body as MemberExpression;
            }

            if (memberExpr == null) throw new ArgumentException(nameof(method));

            return memberExpr;
        }
        #endregion
    }
}