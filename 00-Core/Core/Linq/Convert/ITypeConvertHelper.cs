using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Linq.Convert
{
    /// <summary>
    /// Provides one-directional type conversion helpers.
    /// </summary>
    public interface ITypeConvertHelper
    {
        /// <summary>
        /// Checks whether conversion is valid or not.
        /// </summary>
        /// <param name="type">The source type to convert from.</param>
        /// <returns>A boolean indicating conversion is valid or not.</returns>
        bool CanConvert(Type type);

        /// <summary>
        /// Creates an inline convert expression. (most of the time it's same as CreateLinqToSqlConvertExpression).
        /// </summary>
        /// <param name="source">The source to convert from.</param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        Expression CreateInlineConvertExpression(Expression source, Type destinationType);

        /// <summary>
        /// Creates an inline convert expression safe for linq to sql compilers.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        Expression CreateInlineConvertExpressionQueryableSafe(Expression source, Type destinationType);
    }
}