using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Core.Automation.Mapper;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Linq.Expressions
{
    public static partial class ExpressionHelper
    {
        /// <summary>
        /// Creates a loop over an enumerable, it will optimize to use for instead of foreach wherever possible.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="disposeEnumerator"></param>
        /// <param name="optimizeList"></param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        public static Expression IterateEnumerableInt32Counter(
            Expression collection,
            ParameterExpression loopVar,
            bool disposeEnumerator,
            bool optimizeList,
            params Expression[] loopContents)
        {
            var collectionType = collection.Type;
            if (!collectionType.IsArray &&
                !(optimizeList && collectionType.IsAssignableToGeneric(typeof(IList<>))) &&
                typeof(IEnumerable).IsAssignableFrom(collectionType))
            {
                return ForEach(collection, loopVar, disposeEnumerator, loopContents);
            }

            return ForLoopInt32Counter(collection, loopVar, loopContents);
        }

        /// <summary>
        /// Creates a loop over an enumerable, it will optimize to use for instead of foreach wherever possible.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="disposeEnumerator"></param>
        /// <param name="optimizeList"></param>
        /// <param name="loopBreakLabel"></param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        public static Expression IterateEnumerableInt32Counter(
            Expression collection,
            ParameterExpression loopVar,
            bool disposeEnumerator,
            bool optimizeList,
            LabelTarget loopBreakLabel,
            params Expression[] loopContents)
        {
            var collectionType = collection.Type;
            if (!collectionType.IsArray &&
                !(optimizeList && collectionType.IsAssignableToGeneric(typeof(IList<>))) &&
                typeof(IEnumerable).IsAssignableFrom(collectionType))
            {
                return ForEach(collection, loopVar, disposeEnumerator, loopBreakLabel, loopContents);
            }

            return ForLoopInt32Counter(collection, loopVar, loopBreakLabel, loopContents);
        }


        /// <summary>
        /// Creates a for loop with given body.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        /// <exception cref="MapperCompileException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expression ForLoopInt32Counter(
            Expression collection,
            ParameterExpression loopVar,
            params Expression[] loopContents)
            => ForLoopInt32Counter(collection, loopVar, (LabelTarget) null, loopContents);


        /// <summary>
        /// Creates a for loop with given body.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="loopBreakLabel"></param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        /// <exception cref="MapperCompileException"></exception>
        public static Expression ForLoopInt32Counter(
            Expression collection,
            ParameterExpression loopVar,
            LabelTarget loopBreakLabel,
            params Expression[] loopContents)
        {
            var collectionType = collection.Type;
            var i = Expression.Variable(typeof(int), "i");
            var len = Expression.Variable(typeof(int), "len");

            Expression lenAssign;
            Expression current;

            if (collection.Type.IsArray && collection.Type.GetArrayRank() == 1)
            {
                current = Expression.ArrayIndex(collection, i);
                lenAssign = Expression.Assign(len, Expression.ArrayLength(collection));
            }
            else
            {
                var indexer = collectionType.GetDefaultMembers().OfType<PropertyInfo>()
                    .Where(p =>
                    {
                        var indexParameters = p.GetIndexParameters();
                        return indexParameters.Length == 1 && indexParameters[0].ParameterType == typeof(int);
                    }).SingleOrDefault();
                var countProperty = collectionType.GetProperty(nameof(IList.Count));

                if (indexer == null || countProperty == null)
                {
                    throw new MapperCompileException();
                }

                current = Expression.Property(collection, indexer, i);
                lenAssign = Expression.Assign(len, Expression.Property(collection, countProperty));
            }

            var iAssign = Expression.Assign(i, Expression.Constant(0));

            if (loopBreakLabel == null)
            {
                loopBreakLabel = Expression.Label("LoopBreak");
            }

            var loop = Expression.Block(new[] {i, len},
                iAssign,
                lenAssign,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(i, len),
                        Expression.Block(new[] {loopVar},
                            new[] {Expression.Assign(loopVar, current)}
                                .Concat(loopContents)
                                .Concat(new[] {Expression.PostIncrementAssign(i)})
                        ),
                        Expression.Break(loopBreakLabel)
                    ),
                    loopBreakLabel)
            );

            return loop;
        }


        /// <summary>
        /// Creates a foreach loop with given body.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="disposeEnumerator">Determines whether enumerator's dispose method should be called after loop (it's not dispose safe if exceptions thrown)</param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        public static Expression ForEach(
            Expression collection,
            ParameterExpression loopVar,
            bool disposeEnumerator,
            params Expression[] loopContents)
            => ForEach(collection, loopVar, disposeEnumerator, (LabelTarget) null, loopContents);

        //with help from https://stackoverflow.com/questions/27175558/foreach-loop-using-expression-trees
        /// <summary>
        /// Creates a foreach loop with given body.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="loopVar"></param>
        /// <param name="disposeEnumerator">Determines whether enumerator's dispose method should be called after loop (it's not dispose safe if exceptions thrown)</param>
        /// <param name="loopBreakLabel"></param>
        /// <param name="loopContents"></param>
        /// <returns></returns>
        public static Expression ForEach(
            Expression collection,
            ParameterExpression loopVar,
            bool disposeEnumerator,
            LabelTarget loopBreakLabel,
            params Expression[] loopContents)
        {
            var elementType = loopVar.Type;
            var collectionType = collection.Type;
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
            var enumeratorType = typeof(IEnumerator<>).MakeGenericType(elementType);

            //check whether collection implemented IDisposable
            if (disposeEnumerator)
            {
                disposeEnumerator = typeof(IDisposable).IsAssignableFrom(collectionType);
            }

            //help from mapster
            var isGeneric = enumerableType.IsAssignableFrom(collectionType);
            if (!isGeneric)
            {
                enumerableType = typeof(IEnumerable);
                enumeratorType = typeof(IEnumerator);
            }

            var enumeratorVar = Expression.Variable(enumeratorType, "enumerator");
            var getEnumeratorCall =
                Expression.Call(collection, enumerableType.GetMethod(nameof(IEnumerable.GetEnumerator)));
            var enumerator = Expression.Assign(enumeratorVar, getEnumeratorCall);

            // The MoveNext method's actually on IEnumerator, not IEnumerator<T>
            var moveNextCall =
                Expression.Call(enumeratorVar, typeof(IEnumerator).GetMethod(nameof(IEnumerator.MoveNext)));

            if (loopBreakLabel == null)
            {
                loopBreakLabel = Expression.Label("LoopBreak");
            }

            Expression current = Expression.Property(enumeratorVar, nameof(IEnumerator.Current));

            if (!isGeneric)
            {
                current = Expression.Convert(current, elementType);
            }

            var assignLoopVarToCurrent = Expression.Assign(loopVar, current);

            var loop = Expression.Loop(
                Expression.IfThenElse(
                    moveNextCall,
                    Expression.Block(new[] {loopVar},
                        new[] {assignLoopVarToCurrent}.Concat(loopContents)
                    ),
                    Expression.Break(loopBreakLabel)
                ),
                loopBreakLabel);

            Expression foreachBody;
            if (disposeEnumerator)
            {
                var disposeCall = Expression.Call(enumeratorVar, collectionType.GetMethod(nameof(IDisposable.Dispose)));
                foreachBody = Expression.Block(new[] {enumeratorVar},
                    enumerator,
                    loop,
                    disposeCall
                );
            }
            else
            {
                foreachBody = Expression.Block(new[] {enumeratorVar},
                    enumerator,
                    loop
                );
            }

            return foreachBody;
        }
    }
}