using System;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            /// <summary>
            /// Handles enum mapping in both source and destination.
            /// </summary>
            public class EnumCompiler : PrimitiveCompiler
            {
                static bool _canMap(Type type1, Type type2)
                {
                    if (!type1.IsEnum)
                    {
                        return false;
                    }

                    return type2.IsEnum ||
                           type2 == typeof(string) ||
                           type2 == typeof(byte) ||
                           type2 == typeof(sbyte) ||
                           type2 == typeof(short) ||
                           type2 == typeof(ushort) ||
                           type2 == typeof(int) ||
                           type2 == typeof(uint) ||
                           type2 == typeof(long) ||
                           type2 == typeof(ulong);
                }

                public override bool CanMap(Context context, Type sourceType, Type destinationType)
                {
                    var srcType = sourceType.GetTypeOrNullableBaseType();
                    var dstType = destinationType.GetTypeOrNullableBaseType();

                    return _canMap(srcType, dstType) || _canMap(dstType, srcType);
                }

//                public override Expression CreateCopyExpression(
//                    MapperContext context,
//                    Expression source,
//                    Expression destination,
//                    Mapping mapping,
//                    Mapping.MemberMapInfo memberMapping,
//                    MappingType mappingType)
//                {
//                }

                public override Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType)
                {
                    var srcType = source.Type;
                    if (destinationType == typeof(string))
                    {
                        return _toString(source, srcType);
                    }
                    else if (srcType == typeof(string) && destinationType == typeof(Enum))
                    {
                        return _parse(source, destinationType);
                    }
//                    else if (destinationType.IsEnum && srcType.IsEnum &&
//                             MapEnumByName)
//                    {
//                        return _parse(_toString(source, srcType), destinationType);
//                    }

                    return base.CreateConvertExpression(
                        context,
                        source,
                        destinationType,
                        mappingType);
                }

                private Expression _toString(
                    Expression source,
                    Type sourceType)
                {
                    //source.ToString()
                    var method = sourceType.GetMethod(nameof(ToString));
                    return Expression.Call(method, source);
                }

                private Expression _parse(
                    Expression source,
                    Type destinationType)
                {
                    var method = typeof(Enum).GetMethod(nameof(Enum.Parse), new[] {typeof(Type), typeof(string)});

                    //Enum.Parse(destinationType, source);
                    return Expression.Call(method, Expression.Constant(destinationType), source);
                }
            }
        }
    }
}