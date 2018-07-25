using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Linq.Convert
{
    public abstract class PrimitiveConvertHelper<TPrimitive> : ITypeConvertHelper
    {
        public bool AllowNullableTypes { get; set; }
        
        
        
        protected PrimitiveConvertHelper()
        {
            AllowNullableTypes = true;
        }
        protected PrimitiveConvertHelper(bool allowNullableTypes)
        {
            AllowNullableTypes = allowNullableTypes;
        }

        /// <summary>
        /// Checks whether type is primitive or decimal or if nullable types allowed also checks for nullable primitives and nullable decimal.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>A boolean determining convert capability,</returns>
        public virtual bool CanConvert(Type type)
        {
            return IsPrimitiveOrDecimal(type) ||
                   (AllowNullableTypes && type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(Nullable<>)
                       ? IsPrimitiveOrDecimal(Nullable.GetUnderlyingType(type))
                       : throw TypeConvertHelper.ConvertException(type));
        }

        public virtual Expression CreateInlineConvertExpression(Expression source)
            => AllowNullableTypes
                ? ConvertInlineExpressionAllowNullable(source)
                : ConvertInlineExpressionNoNullable(source);

        public Expression CreateInlineConvertExpressionQueryableSafe(Expression source)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Expression ConvertInlineExpressionAllowNullable(Expression source)
        {
            var convert = source;
            var sourceType = source.Type;
            var destinationType = typeof(TPrimitive);

            if (sourceType != destinationType)
            {
                if (sourceType.IsNullable(out var nullableBaseType))
                    convert = Expression.Convert(convert, nullableBaseType);
                //var destType = destinationType.GetTypeOrNullableBaseType();

//                if (convert.Type != destType)
//                    convert = ConvertType(convert, destType, arg);
                if (convert.Type != destinationType)
                    convert = Expression.Convert(convert, destinationType);

                if (!sourceType.IsValueType || sourceType.IsNullable())
                {
                    //src == null ? default(TDestination) : (TDestination)(src)
                    var compareNull = Expression.Equal(source, Expression.Constant(null, sourceType));
                    convert = Expression.Condition(compareNull, Expression.Default(destinationType), convert);
                }
            }

            return convert;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Expression ConvertInlineExpressionNoNullable(Expression source)
        {
            var destinationType = typeof(TPrimitive);
            
            if (source.Type != destinationType)
            {
                source = Expression.Convert(source, destinationType);
            }

            return source;
        }


        /// <summary>
        /// Checks whether type is primitive or decimal.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>A boolean determining the type is primitive or decimal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrimitiveOrDecimal(Type type)
            => type.IsPrimitive || type == typeof(decimal);

        /// <summary>
        /// Checks whether type is primitive or decimal.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>A boolean determining the type is primitive or decimal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrimitiveOrDecimal(TypeInfo type)
            => type.IsPrimitive || type == typeof(decimal);
    }
}