using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public class Map<TDestination> : IMap
        {
            public MapperConfigurator Configurator { get; }
            
            public Type DestinationType => typeof(TDestination);

            
            public Map(MapperConfigurator configurator)
            {
                Configurator = configurator;
            }
            

            public IEnumerable<MappingRow> CreateRows()
            {
                throw new NotImplementedException();
            }
        }
    }
}