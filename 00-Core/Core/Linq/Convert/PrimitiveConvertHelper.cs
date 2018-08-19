using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Linq.Convert
{
    public class PrimitiveConvertHelper : ITypeConvertHelper
    {
        private static PrimitiveConvertHelper _instance;

        /// <summary>
        /// Default singleton instance.
        /// </summary>
        public static PrimitiveConvertHelper Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                return LazyInitializer.EnsureInitialized(ref _instance);
            }
        }

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
            return type.IsPrimitiveOrDecimal() || type == typeof(string) ||
                   (AllowNullableTypes && type.IsNullable(out var nullableBaseType)
                       ? nullableBaseType.IsPrimitiveOrDecimal()
                       : throw TypeConvertHelper.ConvertException(type));
        }

        public virtual Expression CreateInlineConvertExpression(Expression source, Type destinationType)
            => AllowNullableTypes
                ? ConvertInlineExpressionAllowNullable(source, destinationType)
                : ConvertInlineExpressionNoNullable(source, destinationType);

        public virtual Expression CreateInlineConvertExpressionQueryableSafe(Expression source, Type destinationType)
            => AllowNullableTypes
                ? ConvertInlineExpressionAllowNullable(source, destinationType)
                : ConvertInlineExpressionNoNullable(source, destinationType);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expression ConvertInlineExpressionAllowNullable(Expression source, Type destinationType)
        {
            var convert = source;
            var sourceType = source.Type;

            if (sourceType != destinationType)
            {
                if (sourceType.IsNullable(out var nullableBaseType))
                {
                    convert = Expression.Convert(convert, nullableBaseType);

                    //src == null ? default(TDestination) : {convert}(src)
                    var compareNull = Expression.Equal(convert, Expression.Constant(null, sourceType));
                    convert = Expression.Condition(compareNull, Expression.Default(destinationType), convert);
                }
                else if (sourceType.IsPrimitiveOrDecimal())
                {
                    convert = Expression.Convert(convert, destinationType);
                }
                else if (sourceType.IsConvertible())
                {
                    MethodInfo convertMethodInfo;
                    if (TypeConvertHelper.ConvertMethodNameMapping.TryGetValue(destinationType, out var methodName) &&
                        (convertMethodInfo = typeof(System.Convert).GetMethod(methodName, new[] {sourceType})) != null
                    )
                    {
                        //Convert.To{DestinationType}(source)
                        convert = Expression.Call(convertMethodInfo, convert);
                    }
                    else
                    {
                        convertMethodInfo = typeof(System.Convert).GetMethod(nameof(System.Convert.ChangeType),
                            new[] {typeof(object), typeof(Type)});

                        convert = Expression.Call(convertMethodInfo,
                            Expression.Convert(source, typeof(object)),
                            Expression.Constant(destinationType));

                        convert = Expression.Convert(convert, destinationType);
                    }
                }
                else
                {
                    throw TypeConvertHelper.ConvertException(sourceType, destinationType);
                }
            }

            return convert;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expression ConvertInlineExpressionNoNullable(Expression source, Type destinationType)
        {
            var convert = source;
            var sourceType = source.Type;
            
            if (sourceType != destinationType)
            {
                if (sourceType.IsPrimitiveOrDecimal())
                {
                    convert = Expression.Convert(convert, destinationType);
                }
                else if (sourceType.IsConvertible())
                {
                    MethodInfo convertMethodInfo;
                    if (TypeConvertHelper.ConvertMethodNameMapping.TryGetValue(destinationType, out var methodName) &&
                        (convertMethodInfo = typeof(System.Convert).GetMethod(methodName, new[] {sourceType})) != null
                    )
                    {
                        //Convert.To{DestinationType}(source)
                        convert = Expression.Call(convertMethodInfo, convert);
                    }
                    else
                    {
                        convertMethodInfo = typeof(System.Convert).GetMethod(nameof(System.Convert.ChangeType),
                            new[] {typeof(object), typeof(Type)});

                        convert = Expression.Call(convertMethodInfo,
                            Expression.Convert(source, typeof(object)),
                            Expression.Constant(destinationType));

                        convert = Expression.Convert(convert, destinationType);
                    }
                }
                else
                {
                    throw TypeConvertHelper.ConvertException(sourceType, destinationType);
                }
            }

            return convert;
        }
    }
}