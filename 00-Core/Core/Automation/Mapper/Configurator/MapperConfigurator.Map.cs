using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public class Map : IMap
        {
            public MapperConfigurator Configurator { get; }

            public Type DestinationType { get; }
            
            public Type SourceType { get; }


            public MappingType MappingType { get; protected internal set; }
            public bool IsTwoWay { get; protected internal set; }

            public BindingFlags BindingFlags { get; protected internal set; }

            public ICollection<IModelValueMemberFilter> Filters { get; protected internal set; }


            public Map(MapperConfigurator configurator,
                Type destinationType,
                Type sourceType)
            {
                Configurator = configurator;
                
                DestinationType = destinationType;
                SourceType = sourceType;
            }


            public Map AddBindingPath(
                string destinationPath,
                string sourcePath
            )
            {
                return this;
            }

            public Map UseFlattering()
            {
                return this;
            }

            public Map TwoWay()
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

            public Map FilterMembers(IModelValueMemberFilter filter)
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

//                var row = new Mapping(
//                    DestinationType,
//                    SourceType,
//                );

                //yield return row;
                return null;
            }
        }
    }
}