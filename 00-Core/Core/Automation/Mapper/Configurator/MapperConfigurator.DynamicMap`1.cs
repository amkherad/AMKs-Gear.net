using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public class DynamicMap<TSource> : IMap
        {
            public Type SourceType => typeof(TSource);
            
            
            
            public IEnumerable<MappingRow> CreateRows()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}