using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        // ReSharper disable InconsistentNaming
        public const string CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull = "CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull";
        // ReSharper restore InconsistentNaming
        
        /// <summary>
        /// <see cref="Mapper"/>'s internal default compiler.
        /// </summary>
        public static class Compiler
        {
            [NotNull]
            public static MappingCompiledInfo CompileMapping(Mapper mapper, MapperContext context, Mapping mapping)
            {
                var result = new MappingCompiledInfo();
                
                var destination = Expression.Parameter(mapping.DestinationType);
                var source = Expression.Parameter(mapping.SourceType);

                var memberMappings = new List<Expression>(mapping.MemberMappings.Count);
                
                foreach (var memberMapInfo in mapping.MemberMappings)
                {
                    memberMappings.Add(CreateMemberMapping(context, destination, source, memberMapInfo));
                }

                var defaultConstructor = mapping.DestinationType.GetConstructor(Type.EmptyTypes);
                if (defaultConstructor != null)
                {
                    var newExpression = Expression.MemberInit(Expression.New(defaultConstructor), memberMappings.Cast<MemberBinding>());

                    result.NewMapExpression = newExpression;
                }

                return result;
            }
            
            

            private static Expression CreateMemberMapping(MapperContext context, Expression destination, Expression source, Mapping.MemberMapInfo memberMapInfo)
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
            private static Expression PrimitiveToPrimitive(MapperContext context, Expression destination, Expression source, Mapping.MemberMapInfo memberMapInfo)
            {
                var converter = memberMapInfo.TypeConverter;
                if (converter == null)
                {
                    throw GetCompileException(CreateMemberMapping_PrimitiveToPrimitive_TypeConverterIsNull);
                }

                return converter.CreateInlineConvertExpression(source, memberMapInfo.DestinationMember.Type);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Expression DestinationFromExpression(MapperContext context, Expression destination, Expression source, Mapping.MemberMapInfo memberMapInfo)
            {

                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Expression DestinationFromValueResolver(MapperContext context, Expression destination, Expression source,
                Mapping.MemberMapInfo memberMapInfo)
            {
                
                return null;
            }

            
            
            private static Expression CreateClassMapping(MapperContext context, Type destinationClassType, Type sourceClassType)
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