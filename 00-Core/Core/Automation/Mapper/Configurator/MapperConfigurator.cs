using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator : IDisposable
    {
        public MapperContext Context { get; }

        private ICollection<IMap> _maps;

        public bool CustomMappingEnabled { get; set; }
        public bool CustomMappingCacheEnabled { get; set; }


        public MapperConfigurator(Mapper mapper)
            : this(mapper.Context)
        {
        }

        public MapperConfigurator(MapperContext mapperContext)
        {
            Context = mapperContext ?? throw new ArgumentNullException(nameof(mapperContext));
            _maps = new List<IMap>();
        }


        public Map<TDestination> CreateMap<TDestination>()
        {
            ThrowIfDisposed();

            var map = new Map<TDestination>(this);

            _maps.Add(map);

            return map;
        }


        public Map<TDestination, TSource> CreateMap<TDestination, TSource>()
        {
            ThrowIfDisposed();

            var map = new Map<TDestination, TSource>(this);

            _maps.Add(map);

            return map;
        }

        public Map<TDestination, TSource> CreateMap<TDestination, TSource>(bool twoWay)
        {
            ThrowIfDisposed();

            var map = new Map<TDestination, TSource>(this)
            {
                IsTwoWay = twoWay
            };

            _maps.Add(map);

            return map;
        }

        public Map<TDestination, TSource> CreateMap<TDestination, TSource>(MappingType mappingType)
        {
            ThrowIfDisposed();

            var map = new Map<TDestination, TSource>(this)
            {
                MappingType = mappingType
            };

            _maps.Add(map);

            return map;
        }


        /// <summary>
        /// Removes a map from context. (both destination-to-source and source-to-destination)
        /// </summary>
        /// <remarks>
        /// Maps inside current <see cref="MapperConfigurator"/> don't get removed.
        /// </remarks>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        public void RemoveMap<TDestination, TSource>() => RemoveMap<TDestination, TSource>(true);
        
        /// <summary>
        /// Removes a map from context.
        /// </summary>
        /// <remarks>
        /// Maps inside current <see cref="MapperConfigurator"/> don't get removed.
        /// </remarks>
        /// <param name="twoWay">If true, removes both destination-to-source and source-to-destination maps.</param>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        public void RemoveMap<TDestination, TSource>(bool twoWay)
        {
            Context.Remove(typeof(TDestination), typeof(TSource));

            if (twoWay)
            {
                Context.Remove(typeof(TSource), typeof(TDestination));
            }
        }


        public MapperConfigurator AllowCustomMapping(bool state)
        {
            CustomMappingEnabled = state;
            return this;
        }

        public MapperConfigurator AllowCustomMappingCache(bool state)
        {
            CustomMappingCacheEnabled = state;
            return this;
        }


        public bool Validate()
        {
            return false;
        }


        public void Done()
        {
            if (_maps == null) return;

            var rows = new List<Mapping>();

            foreach (var map in _maps)
            {
                rows.AddRange(map.CreateRows());
            }

            //single call to ensure locking.
            Context.AddRange(rows);
        }

        public void Dispose()
        {
            Done();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (_maps == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}