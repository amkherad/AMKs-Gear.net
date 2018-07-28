using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapping
    {
        public class MemberMapInfo
        {
            public IModelValueMemberInfo Destination { get; set; }
            public IModelValueMemberInfo Source { get; set; }
            
            
        }
    }
}