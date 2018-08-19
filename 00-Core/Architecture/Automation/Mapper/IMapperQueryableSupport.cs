using System;
using System.Linq;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Provides projection capability for a mapper.
    /// </summary>
    public interface IMapperQueryableSupport
    {
        /// <summary>
        /// Projects a source queryable of srcType to a queryable of destType.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="sourceType"></param>
        /// <param name="source"></param>
        /// <param name="options">Mapper engine specific options.</param>
        /// <returns></returns>
        IQueryable Project(Type destinationType, Type sourceType, IQueryable source, object[] options);
        
        /// <summary>
        /// Creates a projection query from source to destination.
        /// </summary>
        /// <param name="source">Source queryable.</param>
        /// <param name="options">Mapper engine specific options.</param>
        /// <typeparam name="TDestination">Result queryable inner type.</typeparam>
        /// <typeparam name="TSource">Source queryable inner type.</typeparam>
        /// <returns></returns>
        IQueryable<TDestination> Project<TDestination, TSource>(IQueryable<TSource> source, object[] options);
    }
}