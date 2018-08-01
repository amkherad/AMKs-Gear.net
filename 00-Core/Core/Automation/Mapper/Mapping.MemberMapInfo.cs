using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Linq.Convert;

namespace AMKsGear.Core.Automation.Mapper
{
    public partial class Mapping
    {
        public enum MemberMapType
        {
            /// <summary>
            /// Primitive means primitives + decimal + string.
            /// </summary>
            PrimitiveToPrimitive,
            
            DestinationFromExpression,
            
            DestinationFromValueResolver,
            
            
        }
        
        public class MemberMapInfo
        {
            public IModelValueMemberInfo DestinationMember { get; set; }
            public IModelValueMemberInfo SourceMember { get; set; }
            
            public ITypeConvertHelper TypeConverter { get; set; }
            
            public MemberMapType MemberMapType { get; set; }
        }
    }
}