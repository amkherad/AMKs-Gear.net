using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public class Map<TDestination, TSource> : IMap
        {
            public MapperConfigurator Configurator { get; }

            public Type DestinationType => typeof(TDestination);
            public Type SourceType => typeof(TSource);


            public MappingType MappingType { get; protected internal set; }
            public bool IsTwoWay { get; protected internal set; }
            
            
            public Map(MapperConfigurator configurator)
            {
                Configurator = configurator;
            }


            public Map<TDestination, TSource> AddBindingPath(
                Expression<Func<TDestination, object>> destinationPath,
                Expression<Func<TSource, object>> sourcePath
                )
            {

                return this;
            }

            public Map<TDestination, TSource> UseFlattering()
            {

                return this;
            }
            
            
            /// <summary>
            /// Creates a list of <see cref="MappingRow"/> from current mapping.
            /// </summary>
            /// <returns></returns>
            public IEnumerable<MappingRow> CreateRows()
            {
                var row = new MappingRow(typeof(TDestination), typeof(TSource));

                yield return row;
            }


            public Map<TDestination, TSource> TwoWay()
            {
                IsTwoWay = true;
                return this;
            }
        }
    }
}