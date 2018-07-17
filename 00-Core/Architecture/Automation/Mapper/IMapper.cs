using System;
using System.Collections;
using System.Collections.Generic;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Automation.Mapper
{
    /// <summary>
    /// Provides a basic mapper functionality.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// One directional mapping from source to destination using given types.
        /// </summary>
        /// <remarks>
        /// destType can differ from destination's type and so for source.
        /// class A { public string Name { get; set; } }
        /// class B : A { public string Title { get; set; } }
        ///
        /// var a = new B();
        /// var b = new B();
        ///
        /// SourceToDestination(typeof(A), b, typeof(B), a); //Title will not be mapped.
        /// </remarks>
        /// <param name="destType">Destination object type.</param>
        /// <param name="destination">Destination object.</param>
        /// <param name="srcType">Source object type.</param>
        /// <param name="source">Source object.</param>
        /// <param name="options">Extended mapper engine options.</param>
        void SourceToDestination([NotNull] Type destType, [NotNull] object destination, [NotNull] Type srcType, [NotNull] object source, object[] options);
        
        
        /// <summary>
        /// One directional mapping from source to destination using given types.
        /// </summary>
        /// <param name="destination">The destination object.</param>
        /// <param name="source">The source object.</param>
        /// <param name="options">Extended mapper engine options.</param>
        /// <typeparam name="TDestination">The destination object type.</typeparam>
        /// <typeparam name="TSource">The source object type.</typeparam>
        void SourceToDestination<TDestination, TSource>([NotNull] TDestination destination, [NotNull] TSource source, object[] options);

        
        /// <summary>
        /// One directional mapping from value provider to destination using given types.
        /// </summary>
        /// <param name="destType">Destination object type.</param>
        /// <param name="destination">Destination object.</param>
        /// <param name="valueProvider">An object to provide custom values from different sources.</param>
        /// <param name="options">Extended mapper engine options.</param>
        void SourceToDestination([NotNull] Type destType, [NotNull] object destination, [NotNull] IMapperValueProvider valueProvider, object[] options);
        
        
        /// <summary>
        /// Projects an enumerable of source types to destination types.
        /// </summary>
        /// <param name="destType"></param>
        /// <param name="srcType"></param>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable Project([NotNull] Type destType, [CanBeNull] Type srcType, [NotNull] IEnumerable source, object[] options);
        
        /// <summary>
        /// Projects an enumerable of source types to destination types.
        /// </summary>
        /// <param name="source">The source enumerable to map to destination enumerable.</param>
        /// <param name="options"></param>
        /// <typeparam name="TDestination">The destination enumerable inner type.</typeparam>
        /// <typeparam name="TSource">The source enumerable inner type.</typeparam>
        /// <returns></returns>
        IEnumerable<TDestination> Project<TDestination, TSource>(IEnumerable<TSource> source, object[] options);
    }
}