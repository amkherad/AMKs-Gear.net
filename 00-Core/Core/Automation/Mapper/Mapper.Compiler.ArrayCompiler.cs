using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Core.Automation.Reflection;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapper
    {
        public static partial class Compiler
        {
            public class ArrayCompiler : ITypeCompiler
            {
                public virtual bool CanMap(Context context, Type sourceType, Type destinationType)
                {
                    return destinationType.IsArray &&
                           typeof(IEnumerable).IsAssignableFrom(sourceType);
                }

                public virtual Expression CreateCopyExpression(
                    Context context,
                    Expression source,
                    Expression destination,
                    MappingType mappingType)
                {
                    if (mappingType == MappingType.ObjectMap)
                    {
                        var sourceType = source.Type;
                        var destinationType = destination.Type;

                        if (sourceType.IsArray)
                        {
                            if (destinationType.GetArrayRank() > 1)
                            {
                                if (sourceType.GetArrayRank() > 1)
                                {
                                    throw new NotImplementedException();
                                }
                                else
                                {
                                    throw new NotImplementedException();
                                }
                            }
                            else
                            {
                                if (sourceType.GetArrayRank() > 1)
                                {
                                    throw new NotImplementedException();
                                }
                                else
                                {
                                    return _copyLinearArrayExpression(source, destination);
                                }
                            }
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        //TODO: localization
                        throw new MapperCompileException();
                    }
                }


                public virtual Expression CreateConvertExpression(
                    Context context,
                    Expression source,
                    Type destinationType,
                    MappingType mappingType)
                {
                    if (mappingType == MappingType.QueryableProjection)
                    {
                        if (destinationType != null)
                        {
                            if (destinationType != source.Type.GetEnumerableBaseType())
                            {
                                throw new MapperCompileException();
                            }
                        }
                    }

                    if (!typeof(IEnumerable).IsAssignableFrom(source.Type))
                    {
                        throw new MapperCompileException();
                    }

                    //source.ToArray()
                    var toArray = Expression.Call(
                        typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray)),
                        source
                    );

                    return toArray;
                }


                private Expression _copyLinearArrayFromEnumerableExpression(
                    Context context,
                    Expression source,
                    Expression destination,
                    bool safeCheckDestinationLength,
                    MappingType mappingType
                )
                {
                    //var index = 0;
                    //var destLength = destination.Length;
                    //foreach(var item in source)
                    //{
                    //    if (index >= destLength) break; //if safeCheckDestinationLength == true
                    //    destination[index++] = convert(item);
                    //}

                    var sourceElementType = source.Type.GetEnumerableBaseType();
                    var destinationElementType = destination.Type.GetEnumerableBaseType();

                    var item = Expression.Variable(sourceElementType, "item");
                    var index = Expression.Variable(typeof(int), "index"); //var index = 0;
                    ParameterExpression destLength = null;

                    if (safeCheckDestinationLength)
                    {
                        destLength =
                            Expression.Variable(typeof(int), "destLength"); //var destLength = destination.Length;
                    }

                    var start = Expression.Assign(index, Expression.Constant(0));
                    var getter =
                        CreateValueConvertExpression(context, item, destinationElementType,
                            mappingType); //convert(item);

                    var set = Expression.Assign(
                        Expression.ArrayAccess(destination, Expression.PostIncrementAssign(index)),
                        getter); //destination[index++] = item;

                    if (safeCheckDestinationLength)
                    {
                        var breakLabel = Expression.Label("LoopBreak");

                        //if (index >= destLength) break;
                        var check = Expression.IfThen(Expression.GreaterThanOrEqual(index, destLength),
                            Expression.Break(breakLabel));

                        var loop = ExpressionHelper.IterateEnumerableInt32Counter(source, item, true, true, breakLabel,
                            set, check);

                        return Expression.Block(new[] {index, destLength}, start, loop);
                    }
                    else
                    {
                        var loop = ExpressionHelper.IterateEnumerableInt32Counter(source, item, true, true, set);

                        return Expression.Block(new[] {index}, start, loop);
                    }
                }

                private Expression _copyLinearArrayExpression(
                    Expression source,
                    Expression destination
                )
                {
                    //Array.Copy(source, destination, Math.Floor(source.Length, destination.Length))

                    //Math.Min(source.Length, destination.Length)
                    var length = Expression.Call(
                        typeof(System.Math).GetMethod(nameof(System.Math.Min),
                            new[] {typeof(int), typeof(int)}), //int Math.Min(int)
                        Expression.ArrayLength(source), //source.Length
                        Expression.ArrayLength(destination) //destination.Length
                    );

                    var block = Expression.Call(
                        typeof(Array).GetMethod(nameof(Array.Copy),
                            new[] {typeof(Array), typeof(Array), typeof(int)}), //Array.Copy(Array, Array, int)
                        source,
                        destination,
                        length
                    );

                    return block;
                }
            }
        }
    }
}