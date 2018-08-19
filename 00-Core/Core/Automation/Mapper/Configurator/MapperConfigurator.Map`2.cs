using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            public BindingFlags DestinationBindingFlags { get; protected internal set; }
            public BindingFlags SourceBindingFlags { get; protected internal set; }

            public ICollection<IModelValueMemberFilter> DestinationFilters { get; protected internal set; }
            public ICollection<IModelValueMemberFilter> SourceFilters { get; protected internal set; }
            
            public IMapperMemberMatchingStrategy MemberMatchingStrategy { get; protected internal set; }
            
            
            public Map(MapperConfigurator configurator)
            {
                Configurator = configurator;

                DestinationBindingFlags = BindingFlags.Public | BindingFlags.Instance;
                SourceBindingFlags = BindingFlags.Public | BindingFlags.Instance;
            }


            public Map<TDestination, TSource> AddBindingPath(
                Expression<Func<TDestination, object>> destinationPath,
                Expression<Func<TSource, object>> sourcePath
                )
            {

                return this;
            }

            public Map<TDestination, TSource> UseFlattening()
            {

                return this;
            }
            
            public Map<TDestination, TSource> TwoWay()
            {
                IsTwoWay = true;
                return this;
            }

//            public Map<TDestination, TSource> FilterMembers(BindingFlags bindingFlags)
//            {
//                BindingFlags = bindingFlags;
//                return this;
//            }

//            public Map<TDestination, TSource> FilterMembers(IModelValueMemberFilter filter)
//            {
//                if (filter == null) throw new ArgumentNullException(nameof(filter));
//                
//                var filters = Filters ?? (Filters = new List<IModelValueMemberFilter>());
//
//                filters.Add(filter);
//                
//                return this;
//            }
            
            
            /// <summary>
            /// Creates a list of <see cref="Mapping"/> from current mapping.
            /// </summary>
            /// <returns></returns>
            public IEnumerable<Mapping> CreateRows()
            {
                var row = new Mapping(
                    typeof(TDestination),
                    typeof(TSource),
                    MapperHelpers.GetMemberMappings(
                        typeof(TDestination),
                        typeof(TSource),
                        DestinationBindingFlags,
                        SourceBindingFlags,
                        MemberMatchingStrategy ?? MapperMemberExactMatchingStrategy.Instance,
                        DestinationFilters,
                        SourceFilters,
                        MappingType
                        )
                    );

                return new[] {row};
            }
        }
    }
}