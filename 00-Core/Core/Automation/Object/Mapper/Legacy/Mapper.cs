using System;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.Mapper;

namespace AMKsGear.Core.Automation.Object.Mapper
{
    public static class Mapper
    {
        #region Factory
        private static readonly IMapperFactory DefaultFactory = new DefaultFactory();
        private static IMapperFactory _factory = DefaultFactory;
        private static bool _lazyLoadMapper = true;
        private static IMapper _lazyLoadedMapper;
        private static readonly object _lazyLoadedMapperConfig = null;
        private static bool _lazyLoadOnNullConfigOnly = true;

        public static void RegisterFactory(IMapperFactory factory, bool lazyLoadMapper = true, bool lazyLoadOnNullConfigOnly = true)
        {
            _factory = factory == null
                ? DefaultFactory
                : _factory;
            _lazyLoadMapper = lazyLoadMapper;
            _lazyLoadedMapper = null;
            _lazyLoadOnNullConfigOnly = lazyLoadOnNullConfigOnly;
        }
        
        public static IMapper CreateDefault(object config = null) => DefaultFactory.CreateInstance(config);
        
        public static IMapper Create(object config = null)
        {
            var mapper = _lazyLoadedMapper;
            if (mapper == null || config != _lazyLoadedMapperConfig)
            {
                mapper = (_factory ?? DefaultFactory).CreateInstance(config);
                if (_lazyLoadMapper && (!_lazyLoadOnNullConfigOnly || config == null))
                    _lazyLoadedMapper = mapper;
            }
            return mapper;
        }
        #endregion

        #region Mapper
        public static TDestination Map<TDestination>(object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destType = typeof(TDestination);
            var srcType = source.GetType();
            //var instance = TypeResolver.CreateInstance<TDestination>(); //new TDestination();

            object result = null;
            Map(destType, ref result, srcType, source);

            return (TDestination)result;
        }
        public static TDestination MapConfig<TDestination>(object source, object config)
        //where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destType = typeof(TDestination);
            var srcType = source.GetType();
            //var instance = TypeResolver.CreateInstance<TDestination>(); //new TDestination();

            object result = null;
            Map(destType, ref result, srcType, source, config);

            return (TDestination)result;
        }
        public static TDestination Map<TDestination, TSource>(TSource source)
        //where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destType = typeof(TDestination);
            var srcType = typeof(TSource);
            //var instance = TypeResolver.CreateInstance<TDestination>(); //new TDestination();

            object result = null;
            Map(destType, ref result, srcType, source);

            return (TDestination)result;
        }
        public static TDestination MapConfig<TDestination, TSource>(TSource source, object config)
        //where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destType = typeof(TDestination);
            var srcType = typeof(TSource);
            //var instance = TypeResolver.CreateInstance<TDestination>(); //new TDestination();

            object result = null;
            Map(destType, ref result, srcType, source, config);

            return (TDestination)result;
        }
        public static void Map(ref object destination, object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            Map(destination.GetType(), ref destination, source.GetType(), source);
        }
        public static void MapConfig(ref object destination, object source, object config)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            Map(destination.GetType(), ref destination, source.GetType(), source, config);
        }
        public static void Map(object destination, object source)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (source == null) throw new ArgumentNullException(nameof(source));
            Map(destination.GetType(), ref destination, source.GetType(), source);
        }
        public static void MapConfig(object destination, object source, object config)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (source == null) throw new ArgumentNullException(nameof(source));
            Map(destination.GetType(), ref destination, source.GetType(), source, config);
        }
        public static void Map<TDestination, TSource>(ref TDestination destination, TSource source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            object result = destination;
            Map(typeof(TDestination), ref result, typeof(TSource), source);
            destination = (TDestination)result;
        }
        public static void MapConfig<TDestination, TSource>(ref TDestination destination, TSource source, object config)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            object result = destination;
            Map(typeof(TDestination), ref result, typeof(TSource), source, config);
            destination = (TDestination)result;
        }
        public static void Map<TDestination, TSource>(TDestination destination, TSource source)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (source == null) throw new ArgumentNullException(nameof(source));
            object result = destination;
            Map(typeof(TDestination), ref result, typeof(TSource), source);
        }
        public static void MapConfig<TDestination, TSource>(TDestination destination, TSource source, object config)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (source == null) throw new ArgumentNullException(nameof(source));
            object result = destination;
            Map(typeof(TDestination), ref result, typeof(TSource), source, config);
        }
        public static void Map(Type destType, ref object destination, Type sourceType, object source, object config = null)
        {
            if (destType == null) throw new ArgumentNullException(nameof(destType));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (sourceType == null) throw new ArgumentNullException(nameof(sourceType));

            using (var mapper = Create(config))
                destination = mapper.SourceToDestination(destType, destination, sourceType, source);
        }
        public static void Map(Type destType, object destination, Type sourceType, object source, object config = null)
        {
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (destType == null) throw new ArgumentNullException(nameof(destType));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (sourceType == null) throw new ArgumentNullException(nameof(sourceType));

            using (var mapper = Create(config))
                mapper.SourceToDestination(destType, destination, sourceType, source);
        }
        #endregion
    }

    public class DefaultFactory : IMapperFactory
    {
        public IMapper CreateInstance(object config) => new DefaultMapper(config as DefaultMapper.Configuration ?? DefaultMapper.Configuration.Default);
    }
}