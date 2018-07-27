using System;
using System.Linq;
using AMKsGear.Architecture.Automation.Mapper;
using AMKsGear.Core.Automation.Mapper.Configurator;

namespace AMKsGear.Core.Automation.Mapper
{
    public static class MapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object @object, params object[] options)
        {
            return default(TDestination);
        }

        public static TDestination ProjectTo<TDestination, TSource>(this TSource source, params object[] options)
        {
            return default(TDestination);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination, TSource>(this IQueryable<TSource> source,
            params object[] options)
        {
            return default(IQueryable<TDestination>);
        }


        public static MapperConfigurator Config(this IMapper mapper) =>
            new MapperConfigurator(mapper as Mapper ?? throw new InvalidOperationException());

        public static MapperConfigurator Config(this Mapper mapper) => new MapperConfigurator(mapper);

        public static MapperConfigurator Config(this MapperContext context) => new MapperConfigurator(context);
    }
}