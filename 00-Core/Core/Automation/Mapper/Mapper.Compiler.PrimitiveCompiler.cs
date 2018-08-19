using System;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Core.Automation.Reflection;
using AMKsGear.Core.Linq.Convert;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            public class PrimitiveCompiler : ITypeCompiler
            {
                private static bool _canMap(Type type, bool allowNullableTypes)
                {
                    if (allowNullableTypes)
                    {
                        if (type.IsNullable(out var nullableBaseType))
                        {
                            type = nullableBaseType;
                        }
                    }

                    return type.IsPrimitiveOrDecimal() || type == typeof(string);
                }

                public virtual bool CanMap(Context context, Type sourceType, Type destinationType)
                {
                    var allowNullableTypes = context.AllowNullableTypes;
                    return _canMap(sourceType, allowNullableTypes) &&
                           _canMap(destinationType, allowNullableTypes);
                }


                public virtual Expression CreateCopyExpression(
                    Context context,
                    Expression source,
                    Expression destination,
                    MappingType mappingType)
                {
                    var convertExpression = CreateConvertExpression(
                        context,
                        source,
                        destination.Type,
                        mappingType);

                    return Expression.Assign(destination, convertExpression);
                }

                public virtual Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType)
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
                            if (TypeConvertHelper.ConvertMethodNameMapping.TryGetValue(destinationType,
                                    out var methodName) &&
                                (convertMethodInfo =
                                    typeof(Convert).GetMethod(methodName, new[] {sourceType})) != null
                            )
                            {
                                //Convert.To{DestinationType}(source)
                                convert = Expression.Call(convertMethodInfo, convert);
                            }
                            else
                            {
                                convertMethodInfo = typeof(Convert).GetMethod(nameof(Convert.ChangeType),
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
    }
}