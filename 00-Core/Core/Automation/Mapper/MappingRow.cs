using System;
using System.ComponentModel;
using System.Linq.Expressions;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// Represents a mapping between two types.
    /// </summary>
    [ImmutableObject(true)]
    [Immutable]
    public class MappingRow : IEquatable<MappingRow>
    {
        /// <summary>
        /// The destination side of the mapping.
        /// </summary>
        public Type DestinationType { get; }
        
        /// <summary>
        /// The source side of the mapping.
        /// </summary>
        public Type SourceType { get; }


        public Expression MapExpression { get; internal set; }
        public Action<object, object> MapFunction { get; internal set; }
        
        
        /// <summary>
        /// Creates a new instance of <see cref="MappingRow"/>.
        /// </summary>
        /// <param name="destinationType">The destination side of the mapping.</param>
        /// <param name="sourceType">The source side of the mapping.</param>
        public MappingRow(Type destinationType, Type sourceType)
        {
            if (destinationType == null) throw new ArgumentNullException(nameof(destinationType));
            
            DestinationType = destinationType;
            SourceType = sourceType;
        }
        
        /// <summary>
        /// Checks equality of this instance of <see cref="MappingRow"/> to another.
        /// </summary>
        /// <param name="other">The other instance of <see cref="MappingRow"/> to compare against.</param>
        /// <returns>A boolean indicating the equality of this instance and other <see cref="MappingRow"/>.</returns>
        public bool Equals(MappingRow other)
        {
            if (ReferenceEquals(other, null)) return false;
            
            return DestinationType == other.DestinationType && SourceType == other.SourceType;
        }

        /// <summary>
        /// Checks equality of this instance of <see cref="MappingRow"/> to another object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MappingRow) obj);
        }

        /// <summary>
        /// Generates a djb2 hash of DestinationType and SourceType without initial seed.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            //var dstHashCode = DestinationType?.GetHashCode() ?? 0;
            var dstHashCode = DestinationType.GetHashCode();
            
            //djb2 hash
            //Destination.HashCode * 33 + Source.HashCode
            unchecked
            {
                return ((dstHashCode << 5) + dstHashCode) ^ (SourceType?.GetHashCode() ?? 0);
            }
        }
    }
}