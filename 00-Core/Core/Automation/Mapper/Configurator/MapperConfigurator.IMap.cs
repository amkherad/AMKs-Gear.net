using System.Collections.Generic;

namespace AMKsGear.Core.Automation.Mapper.Configurator
{
    public partial class MapperConfigurator
    {
        public interface IMap
        {
            IEnumerable<Mapping> CreateRows();
        }
    }
}