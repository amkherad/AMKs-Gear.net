using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MapperContext : ICloneable
    {
        public IDictionary<MappingTrackerRowKey, MappingTrackerRow> MappingTracker { get; }

        
        public MapperContext()
        {
            MappingTracker = new Dictionary<MappingTrackerRowKey, MappingTrackerRow>();            
        }
        
        
        public object Clone()
        {

            return this;
        }
    }
}