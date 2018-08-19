using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Automation.Dependency;
using AMKsGear.Core.Automation.Mapper.Configurator;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            public class ClassCompiler : ITypeCompiler
            {
                public bool CanMap(Context context, Type sourceType, Type destinationType)
                {
                    if (destinationType.IsNullable() ||
                        destinationType.IsConvertible() ||
                        destinationType.IsArray ||
                        destinationType.IsPrimitive)
                        return false;

                    var result = destinationType.GetFields(BindingFlags.Public | BindingFlags.Instance).Any() ||
                           destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Any();

                    return result;
                }


                public Expression CreateInstantiationExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType)
                {
                    var mapperContext = context.MapperContext;

                    if (mappingType == MappingType.ObjectMap)
                    {
                        if (mapperContext.UseDependencyResolverForObjectMapping)
                        {
                            var getMethod =
                                typeof(IDependencyResolver).GetMethod(nameof(IDependencyResolver.Get), Type.EmptyTypes);

                            //context.resolver.Get(destinationType)
                            return Expression.Call(
                                Expression.Property(Expression.Constant(mapperContext),
                                    typeof(MapperContext).GetProperty(nameof(MapperContext.DependencyResolver))),
                                getMethod,
                                Expression.Constant(destinationType)
                            );
                        }
                    }

                    if (!destinationType.TryGetDefaultConstructor(out var constructorInfo))
                    {
                        throw new MapperCompileException();
                    }

                    return Expression.New(constructorInfo);
                }

                public NewExpression CreateNewExpression(Context context, Expression source, Type destinationType,
                    MappingType mappingType)
                {
                    if (!destinationType.TryGetDefaultConstructor(out var constructorInfo))
                    {
                        throw new MapperCompileException();
                    }

                    return Expression.New(constructorInfo);
                }


                public Expression CreateCopyExpression(
                    Context context,
                    Expression source,
                    Expression destination,
                    MappingType mappingType)
                {
                    return null;
                }

                public Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType
                )
                {
                    //TODO: depth & preserve references

                    var sourceType = source.Type;

                    var mapperContext = context.MapperContext;
                    var mapping = context.Mapping;

                    var instance = CreateNewExpression(context, source, destinationType, mappingType);

                    List<Mapping.MemberMapInfo> memberMappings = null;

                    if (mapping.SourceType == sourceType && mapping.DestinationType == destinationType)
                    {
                        memberMappings = mapping.MemberMappings;
                    }

                    if (memberMappings == null)
                    {
                        if (mapperContext.TryGetMapping(
                            destinationType,
                            sourceType,
                            out var newMapping))
                        {
                            memberMappings = newMapping.MemberMappings;
                            mapping = newMapping;
                        }
                        else
                        {
                            memberMappings = MapperHelpers.GetMemberMappings(
                                destinationType,
                                sourceType,
                                BindingFlags.Public,
                                BindingFlags.Public,
                                mapperContext.MemberMatchingStrategy,
                                null,
                                null,
                                mappingType
                            );
                        }

                        if (memberMappings == null)
                        {
                            throw new MapperCompileException();
                        }
                    }

                    return CreateConvertExpression(
                        context,
                        source,
                        destinationType,
                        instance,
                        memberMappings,
                        mappingType
                    );
                }

                public static Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    NewExpression newInstanceExpression,
                    List<Mapping.MemberMapInfo> memberMappings,
                    MappingType mappingType)
                {
                    var bindings = new List<MemberBinding>(memberMappings.Count);

                    foreach (var member in memberMappings)
                    {
                        var convert = CreateValueConvertExpression(context,
                            member.SourceMember.CreateGetExpression(source), member.DestinationType, mappingType);

                        var bind = Expression.Bind(member.DestinationMember.MemberInfo, convert);

                        bindings.Add(bind);
                    }

                    return Expression.MemberInit(newInstanceExpression, bindings);
                }
            }
        }
    }
}