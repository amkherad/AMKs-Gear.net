using System;
using System.Linq.Expressions;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Provides access to extended mapper functions.
    /// </summary>
    public interface IMapperQueryableSupportEx
    {
        Expression GetProjectionExpression(Type destType, Type srcType, object[] options);
        Expression<Func<TSource, TDestination>> GetProjectionExpression<TDestination, TSource>(object[] options);
    }
}