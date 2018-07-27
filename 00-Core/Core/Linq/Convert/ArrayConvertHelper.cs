using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Linq.Convert
{
    public class ArrayConvertHelper : ITypeConvertHelper
    {
        public bool CanConvert(Type type)
            => typeof(IEnumerable).IsAssignableFrom(type);


        /// <summary>
        /// Create an expression to call <c>Array.Copy</c> to copy arrays.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="destination">The destination array.</param>
        /// <returns></returns>
        /// <exception cref="TypeConvertException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static Expression CreateCopyArrayExpression(Expression source, Expression destination)
            => CreateCopyArrayExpression(source, destination, null);

        /// <summary>
        /// Create an expression to call <c>Array.Copy</c> to copy arrays.
        /// </summary>
        /// <param name="arraySource">The source array.</param>
        /// <param name="arrayDestination">The destination array.</param>
        /// <param name="length">A nullable length expression, if null passes </param>
        /// <returns></returns>
        /// <exception cref="TypeConvertException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static Expression CreateCopyArrayExpression(Expression arraySource, Expression arrayDestination, Expression length)
        {
            if (!arraySource.Type.IsArray || !arrayDestination.Type.IsArray)
            {
                throw TypeConvertHelper.ConvertException(arraySource.Type, arrayDestination.Type);
            }

            var sourceElementType = arraySource.Type.GetElementType().GetTypeOrNullableBaseType();

            //TODO: add IConvertible support
            //TSource must be array and same as TDestination
            //Source's rank must be 1
            //TSource must be primitive or decimal or object
            if (arraySource.Type.GetElementType() == arrayDestination.Type.GetElementType() &&
                arraySource.Type.GetArrayRank() == 1 &&
                (
                    sourceElementType.IsPrimitive ||
                    sourceElementType == typeof(object) ||
                    sourceElementType == typeof(decimal)
                )
            )
            {
                //Array.Copy(source, destination, Math.Floor(source.Length, destination.Length))

                if (length == null)
                {
                    //Math.Min(source.Length, destination.Length)
                    length = Expression.Call(
                        typeof(System.Math).GetMethod(nameof(System.Math.Min),
                            new[] {typeof(int), typeof(int)}), //int Math.Min(int)
                        Expression.ArrayLength(arraySource), //source.Length
                        Expression.ArrayLength(arrayDestination) //destination.Length
                    );
                }

                var block = Expression.Call(
                    typeof(Array).GetMethod(nameof(Array.Copy),
                        new[] {typeof(Array), typeof(Array), typeof(int)}), //Array.Copy(Array, Array, int)
                    arraySource,
                    arrayDestination,
                    length
                );

                return block;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static Expression CreateToArrayExpression(Expression enumerableSource, Type destinationType)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(enumerableSource.Type))
            {
                throw TypeConvertHelper.ConvertException(enumerableSource.Type, destinationType);
            }

            if (destinationType == null)
            {
                destinationType = enumerableSource.Type.GetEnumerableBaseType();
            }
            
            //source.ToArray()
            var toArray = Expression.Call(
                typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray)),
                enumerableSource
            );

            return toArray;
        }
        
        public Expression CreateInlineConvertExpression(Expression source, Type destinationType)
        {
            throw new NotSupportedException();
        }

        public Expression CreateInlineConvertExpressionQueryableSafe(Expression source, Type destinationType)
        {
            throw new NotSupportedException();
        }
    }
}