using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        // ReSharper disable InconsistentNaming
        public const string CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull =
            "CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull";
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// <see cref="Mapper"/>'s internal default compiler.
        /// </summary>
        public static partial class Compiler
        {
            private static List<ITypeCompiler> Compilers;


            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void _ensureCompilers()
            {
                if (Compilers == null)
                {
                    LazyInitializer.EnsureInitialized(ref Compilers, () =>
                    {
                        return new List<ITypeCompiler>
                        {
                            new EnumCompiler(),
                            new PrimitiveCompiler(),
                            new ArrayCompiler(),
                            new ClassCompiler(),
                        };
                    });
                }
            }


            /// <summary>
            /// Compiles a mapping.
            /// </summary>
            /// <param name="mapper"></param>
            /// <param name="context"></param>
            /// <param name="mapping"></param>
            /// <param name="mappingType"></param>
            /// <returns></returns>
            [NotNull]
            public static MappingCompiledInfo CompileMapping(
                Mapper mapper,
                MapperContext context,
                Mapping mapping,
                MappingType mappingType)
            {
                _ensureCompilers();

                var result = new MappingCompiledInfo();

                var compilerContext = new Context(mapper, context, mapping, result);

                var source = Expression.Parameter(mapping.SourceType, "source");

                switch (mappingType)
                {
                    case MappingType.ObjectMap:
                    {
                        break;
                    }
                    case MappingType.QueryableProjection:
                    {
                        foreach (var compiler in Compilers)
                        {
                            if (compiler.CanMap(compilerContext, mapping.SourceType, mapping.DestinationType))
                            {
                                var exp = compiler.CreateConvertExpression(
                                    compilerContext,
                                    source,
                                    mapping.DestinationType,
                                    mappingType
                                );

                                result.ProjectionExpression = Expression.Lambda(exp, source);
                            }
                        }

                        break;
                    }
                    case MappingType.CollectionSynchronization:
                    {
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mappingType), mappingType, null);
                }

                if (result == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(mappingType), mappingType, null);
                }

                return result;
            }

            [NotNull]
            public static MappingCompiledInfo CompileMapping(
                Mapper mapper,
                MapperContext context,
                MappingCompiledInfo compiledInfo,
                Mapping mapping,
                MappingType mappingType)
            {
                _ensureCompilers();

                var compilerContext = new Context(mapper, context, mapping, compiledInfo);

                var source = Expression.Parameter(mapping.SourceType, "source");

                switch (mappingType)
                {
                    case MappingType.ObjectMap:
                    {
                        if (compiledInfo.ObjectMapExpression != null)
                        {
                            return compiledInfo;
                        }

                        break;
                    }
                    case MappingType.QueryableProjection:
                    {
                        if (compiledInfo.ProjectionExpression != null)
                        {
                            return compiledInfo;
                        }
                        
                        foreach (var compiler in Compilers)
                        {
                            if (compiler.CanMap(compilerContext, mapping.SourceType, mapping.DestinationType))
                            {
                                var exp = compiler.CreateConvertExpression(
                                    compilerContext,
                                    source,
                                    mapping.DestinationType,
                                    mappingType
                                );

                                compiledInfo.ProjectionExpression = Expression.Lambda(exp, source);
                            }
                        }

                        break;
                    }
                    case MappingType.CollectionSynchronization:
                    {
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mappingType), mappingType, null);
                }

                return compiledInfo;
            }


            internal static Expression CreateValueConvertExpression(
                Context context,
                Expression source,
                Type destinationType,
                MappingType mappingType)
            {
                _ensureCompilers();

                foreach (var compiler in Compilers)
                {
                    if (compiler.CanMap(context, source.Type, destinationType))
                    {
                        return compiler.CreateConvertExpression(context, source, destinationType, mappingType);
                    }
                }

                throw new MapperCompileException();
            }


            private static Expression CreateMemberMapping(MapperContext context, Expression destination,
                Expression source, Mapping.MemberMapInfo memberMapInfo)
            {
                switch (memberMapInfo.MemberMapType)
                {
                    case Mapping.MemberMapType.PrimitiveToPrimitive:
                    {
                        return PrimitiveToPrimitive(context, destination, source, memberMapInfo);
                    }
                    case Mapping.MemberMapType.DestinationFromExpression:
                    {
                        return DestinationFromExpression(context, destination, source, memberMapInfo);
                    }
                    case Mapping.MemberMapType.DestinationFromValueResolver:
                    {
                        return DestinationFromValueResolver(context, destination, source, memberMapInfo);
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Expression PrimitiveToPrimitive(MapperContext context, Expression destination,
                Expression source, Mapping.MemberMapInfo memberMapInfo)
            {
                var converter = memberMapInfo.TypeConverter;
                if (converter == null)
                {
                    throw GetCompileException(CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull);
                }

                return converter.CreateInlineConvertExpression(source, memberMapInfo.DestinationMember.Type);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Expression DestinationFromExpression(MapperContext context, Expression destination,
                Expression source, Mapping.MemberMapInfo memberMapInfo)
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Expression DestinationFromValueResolver(MapperContext context, Expression destination,
                Expression source,
                Mapping.MemberMapInfo memberMapInfo)
            {
                return null;
            }


            private static Expression CreateClassMapping(MapperContext context, Type destinationClassType,
                Type sourceClassType)
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static MapperCompileException GetCompileException(string messageId)
            {
                return new MapperCompileException();
            }
        }
    }
}