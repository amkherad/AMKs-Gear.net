using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapping
    {
        public class MemberMapInfo
        {
            public IModelValueMemberInfo DestinationMember { get; set; }
            public IModelValueMemberInfo SourceMember { get; set; }
            
            
        }
    }
}