using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Architecture.Automation.Mapper;
using AMKsGear.Architecture.Linq.Expressions;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// A simple object mapper with support of queryable projection.
    /// </summary>
    public partial class Mapper
        : IMapper, IMapperQueryableSupport,
            IMapperEx, IMapperQueryableSupportEx
    {
        public IExpressionCompiler ExpressionCompiler { get; }
        //public ITypeResolver TypeResolver { get; }

        public MapperContext Context { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Mapper"/>.
        /// </summary>
        /// <param name="expressionCompiler">The compiler to compile expressions.</param>
        /// <param name="mapperContext"></param>
        public Mapper(IExpressionCompiler expressionCompiler, MapperContext mapperContext)
        {
            ExpressionCompiler = expressionCompiler ?? throw new ArgumentNullException(nameof(expressionCompiler));
            Context = mapperContext ?? throw new ArgumentNullException(nameof(mapperContext));
        }

        /// <inheritdoc />
        public Mapper()
            : this(
                InternalExpressionCompiler.Instance,
                new MapperContext()
            )
        {
        }


        /// <summary>
        /// Validates values passed to method, and changes them if required (specified in options).
        /// </summary>
        /// <param name="destType"></param>
        /// <param name="destination"></param>
        /// <param name="srcType"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected bool ValidateParameters(Type destType, object destination, Type srcType, object source,
            object[] options)
        {
            destination = destination;
            source = source;
            return true;
        }

        /// <summary>
        /// Validates values passed to method, and changes them if required (specified in options).
        /// </summary>
        /// <param name="destType"></param>
        /// <param name="srcType"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected bool ValidateParameters(Type destType, Type srcType, object source, object[] options)
        {
            return true;
        }

        /// <summary>
        /// Validates values passed to method, and changes them if required (specified in options).
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected bool ValidateParameters<TDestination, TSource>(TDestination destination, TSource source,
            object[] options)
        {
            destination = destination;
            source = source;
            return true;
        }

        /// <summary>
        /// Validates values passed to method, and changes them if required (specified in options).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected bool ValidateParameters<TDestination, TSource>(IQueryable<TSource> source, object[] options)
        {
            source = source;
            return true;
        }


        public void Compile()
        {
            var context = Context;
            var entries = context.GetMappingsWithoutCompiledInfo();

            var compiled = new Dictionary<int, MappingCompiledInfo>();

            lock (context.CompileLockTarget)
            {
                foreach (var entry in entries)
                {
                    var compiledInfo = Compiler.CompileMapping(this, context, entry.Value, MappingType.ObjectMap);
                    compiled.Add(entry.Key, compiledInfo);
                }
            }

            if (compiled.Count > 0)
            {
                context.CacheCompiledInfos(compiled);
            }
        }


        /// <inheritdoc />
        /// <exception cref="MapperException"></exception>
        [Throws(typeof(MapperException))]
        public void SourceToDestination(Type destType, object destination, Type srcType, object source,
            object[] options)
        {
            if (!ValidateParameters(destType, destination, srcType, source, options))
            {
                ThrowValidationException();
            }

            var mapper = GetMapAction(destType, srcType, options);

            mapper(destination, source);
        }

        /// <inheritdoc />
        /// <exception cref="MapperException"></exception>
        [Throws(typeof(MapperException))]
        public void SourceToDestination<TDestination, TSource>(TDestination destination, TSource source,
            object[] options)
        {
            if (!ValidateParameters(destination, source, options))
            {
                ThrowValidationException();
            }

            var mapper = GetMapAction<TDestination, TSource>(options);

            mapper(destination, source);
        }

        /// <inheritdoc />
        public void SourceToDestination(Type destType, object destination, IMapperValueProvider valueProvider,
            object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable Project(Type destType, Type srcType, IEnumerable source, object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<TDestination> Project<TDestination, TSource>(IEnumerable<TSource> source, object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IQueryable Project(Type destType, Type srcType, IQueryable source, object[] options)
        {
            if (!ValidateParameters(destType, srcType, source, options))
            {
                ThrowValidationException();
            }

            try
            {
                var projectionCall = GetProjectionCallExpression(destType, srcType, options);
                var sourceCall = Expression.Call(projectionCall.Method, source.Expression, projectionCall.Arguments[1]);
                return source.Provider.CreateQuery(sourceCall);
            }
            catch (MapperException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message, ex);
            }
        }

        /// <inheritdoc />
        public IQueryable<TDestination> Project<TDestination, TSource>(IQueryable<TSource> source, object[] options)
        {
            if (!ValidateParameters<TDestination, TSource>(source, options))
            {
                ThrowValidationException();
            }

            try
            {
                var projectionCall = GetProjectionCallExpression<TDestination, TSource>(options);
                var sourceCall = Expression.Call(projectionCall.Method, source.Expression, projectionCall.Arguments[1]);
                return source.Provider.CreateQuery<TDestination>(sourceCall);
            }
            catch (MapperException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MapperException(ex.Message, ex);
            }
        }


        /// <inheritdoc />
        public Func<object, object> GetMapFunction(Type destType, Type srcType, object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Action<object, object> GetMapAction(Type destType, Type srcType, object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Func<TSource, TDestination> GetMapFunction<TDestination, TSource>(object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Action<TDestination, TSource> GetMapAction<TDestination, TSource>(object[] options)
        {
            throw new NotImplementedException();
        }


        protected internal MethodCallExpression GetProjectionCallExpression(
            Type destinationType,
            Type sourceType,
            object[] options)
        {
            var lambda = GetProjectionLambdaExpression(destinationType, sourceType, options);

            var sourceParameter = Expression.Parameter(typeof(IQueryable<>).MakeGenericType(sourceType));
            var methodInfo = (from method in typeof(Queryable).GetMethods()
                where method.Name == nameof(Queryable.Select)
                let p = method.GetParameters()[1]
                where p.ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>)
                select method).First().MakeGenericMethod(sourceType, destinationType);

            return Expression.Call(methodInfo, sourceParameter, Expression.Quote(lambda));
        }

        protected internal MethodCallExpression GetProjectionCallExpression<TDestination, TSource>(object[] options)
        {
            var lambda = GetProjectionLambdaExpression<TDestination, TSource>(options);

            var sourceParameter = Expression.Parameter(typeof(IQueryable<>).MakeGenericType(typeof(TSource)));
            var methodInfo = (from method in typeof(Queryable).GetMethods()
                where method.Name == nameof(Queryable.Select)
                let p = method.GetParameters()[1]
                where p.ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>)
                select method).First().MakeGenericMethod(typeof(TSource), typeof(TDestination));

            return Expression.Call(methodInfo, sourceParameter, Expression.Quote(lambda));
        }


        /// <inheritdoc />
        public Expression GetProjectionExpression(Type destType, Type srcType, object[] options)
        {
            var expression = GetProjectionLambdaExpression(destType, srcType, options) as Expression;

            if (expression == null)
            {
                throw new MapperException();
            }

            return expression;
        }

        /// <inheritdoc />
        public Expression<Func<TSource, TDestination>> GetProjectionExpression<TDestination, TSource>(object[] options)
        {
            var expression =
                GetProjectionLambdaExpression<TDestination, TSource>(options) as
                    Expression<Func<TSource, TDestination>>;

            if (expression == null)
            {
                throw new MapperException();
            }

            return expression;
        }

        public LambdaExpression GetProjectionLambdaExpression(Type destType, Type srcType, object[] options)
        {
            if (destType == null) throw new ArgumentNullException(nameof(destType));
            if (srcType == null) throw new ArgumentNullException(nameof(srcType));

            var context = Context;
            if (context.TryGetMappingAndCompiledInfo(destType, srcType, out var mapping,
                out var mapCompiledInfo))
            {
                if (mapCompiledInfo != null)
                {
                    if (mapCompiledInfo.ProjectionExpression != null)
                    {
                        return mapCompiledInfo.ProjectionExpression;
                    }
                    else
                    {
                        lock (context.CompileLockTarget)
                        {
                            //Double check the compiled context.
                            if (context.TryGetCompiledInfo(destType, srcType,
                                    out mapCompiledInfo) &&
                                mapCompiledInfo.ProjectionExpression != null)
                            {
                                return mapCompiledInfo.ProjectionExpression;
                            }

                            mapCompiledInfo =
                                Compiler.CompileMapping(this, context, mapCompiledInfo, mapping,
                                    MappingType.QueryableProjection);

                            return mapCompiledInfo.ProjectionExpression;
                        }
                    }
                }
                else
                {
                    lock (context.CompileLockTarget)
                    {
                        //Double check the compiled context.
                        if (context.TryGetCompiledInfo(destType, srcType, out mapCompiledInfo) &&
                            mapCompiledInfo.ProjectionExpression != null)
                        {
                            return mapCompiledInfo.ProjectionExpression;
                        }

                        mapCompiledInfo =
                            Compiler.CompileMapping(this, context, mapping, MappingType.QueryableProjection);

                        context.CacheCompiledInfo(destType, srcType, mapCompiledInfo);

                        return mapCompiledInfo.ProjectionExpression;
                    }
                }
            }
            else if (context.AllowOnTheFlyMapping)
            {
                lock (context.CompileLockTarget)
                {
                    mapping = _addDefaultMapping(destType, srcType);

                    mapCompiledInfo = Compiler.CompileMapping(this, context, mapping, MappingType.QueryableProjection);

                    context.CacheCompiledInfo(destType, srcType, mapCompiledInfo);

                    return mapCompiledInfo.ProjectionExpression;
                }
            }
            else
            {
                throw new MappingNotFoundException();
            }
        }

        public LambdaExpression GetProjectionLambdaExpression<TDestination, TSource>(object[] options)
        {
            return GetProjectionLambdaExpression(typeof(TDestination), typeof(TSource), options);
        }


        private Mapping _addDefaultMapping(Type destinationType, Type sourceType)
        {
            //TODO: temporary !

            using (var config = Context.Config())
            {
                config.CreateMap(destinationType, sourceType);
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="callerMemberName"></param>
        /// <exception cref="MapperException"></exception>
        protected void ThrowValidationException([CallerMemberName] string callerMemberName = null)
        {
            throw new MapperException();
        }
    }
}