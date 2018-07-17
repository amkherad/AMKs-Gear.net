using System;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MappingTrackerRowKey : IEquatable<MappingTrackerRowKey>
    {
        public Type DestinationType { get; }
        public Type SourceType { get; }
        
        
        public MappingTrackerRowKey(Type destinationType, Type sourceType)
        {
            DestinationType = destinationType;
            SourceType = sourceType;
        }
        
        
        public bool Equals(MappingTrackerRowKey other)
        {
            if (ReferenceEquals(other, null)) return false;
            
            return DestinationType == other.DestinationType && SourceType == other.SourceType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MappingTrackerRowKey) obj);
        }

        public override int GetHashCode()
        {
            var dstHashCode = DestinationType?.GetHashCode() ?? 0;
            
            //djb2 hash
            //Destination.HashCode * 33 + Source.HashCode
            unchecked
            {
                return ((dstHashCode << 5) + dstHashCode) ^ (SourceType?.GetHashCode() ?? 0);
            }
        }
    }
}