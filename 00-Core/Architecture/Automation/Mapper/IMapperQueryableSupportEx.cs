using System;
using System.Linq.Expressions;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Provides access to extended mapper functions.
    /// </summary>
    public interface IMapperQueryableSupportEx
    {
        Expression GetProjectionExpression(Type destinationType, Type sourceType, object[] options);
        Expression<Func<TSource, TDestination>> GetProjectionExpression<TDestination, TSource>(object[] options);
        
        LambdaExpression GetProjectionLambdaExpression(Type destinationType, Type sourceType, object[] options);
        LambdaExpression GetProjectionLambdaExpression<TDestination, TSource>(object[] options);
    }
}