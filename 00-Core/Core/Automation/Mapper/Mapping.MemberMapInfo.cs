using System;
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
            public IModelValueMemberInfo SourceMember { get; set; }
            public IModelValueMemberInfo DestinationMember { get; set; }

            public ITypeConvertHelper TypeConverter { get; set; }

            public MemberMapType MemberMapType { get; set; }

            public bool AllowNullableSource { get; set; } = true;
            public bool AllowNullableDestination { get; set; } = true;

            public Type SourceType => SourceMember.Type;
            public Type DestinationType => DestinationMember == null ? SourceMember.Type : DestinationMember.Type;
        }
    }
}