using System;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.Mapper;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public static class MapperExtensions
    {
        public static T Map<T>(this IMapper mapper, object source, object config = null)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = default(T);
            mapper.SourceToDestination(typeof(T), result, source.GetType(), source);
            return result;
        }
        public static void Map<TDestination, TSource>(this IMapper mapper,
            TDestination destination, TSource source, object config = null)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (source == null) throw new ArgumentNullException(nameof(source));
            mapper.SourceToDestination(typeof(TDestination), destination, typeof(TSource), source);
        }
    }
}