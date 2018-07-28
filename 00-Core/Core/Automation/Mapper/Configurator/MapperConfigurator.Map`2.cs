using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

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
            
            public BindingFlags BindingFlags { get; protected internal set; }

            public ICollection<IModelValueMemberFilter> Filters { get; protected internal set; }
            
            
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
            
            public Map<TDestination, TSource> TwoWay()
            {
                FilterMembers(null);
                IsTwoWay = true;
                return this;
            }

//            public Map<TDestination, TSource> FilterMembers(BindingFlags bindingFlags)
//            {
//                BindingFlags = bindingFlags;
//                return this;
//            }

            public Map<TDestination, TSource> FilterMembers(IModelValueMemberFilter filter)
            {
                if (filter == null) throw new ArgumentNullException(nameof(filter));
                
                var filters = Filters ?? (Filters = new List<IModelValueMemberFilter>());

                filters.Add(filter);
                
                return this;
            }
            
            /// <summary>
            /// Creates a list of <see cref="Mapping"/> from current mapping.
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Mapping> CreateRows()
            {
                var memberMaps = new List<Mapping.MemberMapInfo>();
                
                var row = new Mapping(
                    typeof(TDestination),
                    typeof(TSource),
                    
                    );

                yield return row;
            }
        }
    }
}