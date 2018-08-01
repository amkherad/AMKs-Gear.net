using System;
using System.Collections.Generic;
using System.Reflection;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public interface IMap
        {
            //MappingType MappingType { get; }
            //
            //Type DestinationType { get; }
            //BindingFlags DestinationBindingFlags { get; }

            IEnumerable<Mapping> CreateRows();
        }
    }
}