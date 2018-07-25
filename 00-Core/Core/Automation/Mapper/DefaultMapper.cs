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
    public class DefaultMapper
        : IMapper, IMapperQueryableSupport,
            IMapperEx, IMapperQueryableSupportEx
    {
        public IExpressionCompiler ExpressionCompiler { get; }
        
        
        /// <summary>
        /// Creates a new instance of <see cref="DefaultMapper"/>.
        /// </summary>
        /// <param name="expressionCompiler">The compiler to compile expressions.</param>
        public DefaultMapper(IExpressionCompiler expressionCompiler)
        {
            ExpressionCompiler = expressionCompiler;
        }

        /// <inheritdoc />
        public DefaultMapper()
            : this(InternalExpressionCompiler.Instance)
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
        protected bool ValidateParameters(Type destType, ref object destination, Type srcType, ref object source, object[] options)
        {
            destination = destination;
            source = source;
            return true;
        }
        
        /// <summary>
        /// Validates values passed to method, and changes them if required (specified in options).
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected bool ValidateParameters<TDestination, TSource>(ref TDestination destination, ref TSource source, object[] options)
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
        
        
        /// <inheritdoc />
        /// <exception cref="MapperException"></exception>
        [Throws(typeof(MapperException))]
        public void SourceToDestination(Type destType, object destination, Type srcType, object source, object[] options)
        {
            if (!ValidateParameters(destType, ref destination, srcType, ref source, options))
            {
                ThrowValidationException();
            }
            
            var mapper = GetMapAction(destType, srcType, options);

            mapper(destination, source);
        }

        /// <inheritdoc />
        /// <exception cref="MapperException"></exception>
        [Throws(typeof(MapperException))]
        public void SourceToDestination<TDestination, TSource>(TDestination destination, TSource source, object[] options)
        {
            if (!ValidateParameters(ref destination, ref source, options))
            {
                ThrowValidationException();
            }
            
            var mapper = GetMapAction<TDestination, TSource>(options);

            mapper(destination, source);
        }

        /// <inheritdoc />
        public void SourceToDestination(Type destType, object destination, IMapperValueProvider valueProvider, object[] options)
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
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IQueryable<TDestination> Project<TDestination, TSource>(IQueryable<TSource> source, object[] options)
        {
            if (!ValidateParameters<TDestination, TSource>(source, options))
            {
                ThrowValidationException();
            }

            var expression = GetProjectionExpression<TDestination, TSource>(options);
            if (expression == null)
            {
                
            }

            throw new NotImplementedException();
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

        /// <inheritdoc />
        public Expression GetProjectionExpression(Type destType, Type srcType, object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Expression<Func<TSource, TDestination>> GetProjectionExpression<TDestination, TSource>(object[] options)
        {
            throw new NotImplementedException();
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