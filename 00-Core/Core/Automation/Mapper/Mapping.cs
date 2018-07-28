using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// Represents a mapping between two types.
    /// </summary>
    [ImmutableObject(true)]
    [Immutable]
    public partial class Mapping : IEquatable<Mapping>
    {
        /// <summary>
        /// The destination side of the mapping.
        /// </summary>
        public Type DestinationType { get; }
        
        /// <summary>
        /// The source side of the mapping.
        /// </summary>
        public Type SourceType { get; }

        
        /// <summary>
        /// A table of member mappings.
        /// </summary>
        public IEnumerable<MemberMapInfo> MemberMappings { get; }


        /// <summary>
        /// Creates a new instance of <see cref="Mapping"/>.
        /// </summary>
        /// <param name="destinationType">The destination side of the mapping.</param>
        /// <param name="sourceType">The source side of the mapping.</param>
        /// <param name="memberMappings"></param>
        public Mapping(
            Type destinationType,
            Type sourceType,
            IEnumerable<MemberMapInfo> memberMappings
            )
        {
            if (destinationType == null) throw new ArgumentNullException(nameof(destinationType));
            
            DestinationType = destinationType;
            SourceType = sourceType;
            MemberMappings = memberMappings;
        }
        
        /// <summary>
        /// Checks equality of this instance of <see cref="Mapping"/> to another.
        /// </summary>
        /// <param name="other">The other instance of <see cref="Mapping"/> to compare against.</param>
        /// <returns>A boolean indicating the equality of this instance and other <see cref="Mapping"/>.</returns>
        public bool Equals(Mapping other)
        {
            if (ReferenceEquals(other, null)) return false;
            
            return DestinationType == other.DestinationType && SourceType == other.SourceType;
        }

        /// <summary>
        /// Checks equality of this instance of <see cref="Mapping"/> to another object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mapping) obj);
        }

        public override int GetHashCode() => ComputeHash(DestinationType, SourceType);

        /// <summary>
        /// Generates a djb2 hash of destinationType and sourceType without initial seed.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComputeHash(Type destinationType, Type sourceType)
        {
            var dstHashCode = destinationType.GetHashCode();
            
            //djb2 hash
            //Destination.HashCode * 33 + Source.HashCode
            unchecked
            {
                return ((dstHashCode << 5) + dstHashCode) ^ (sourceType?.GetHashCode() ?? 0);
            }
        }
        
        /// <summary>
        /// Generates a djb2 hash of destinationType without initial seed.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComputeHash(Type destinationType)
        {
            var dstHashCode = destinationType.GetHashCode();
            
            //djb2 hash
            //Destination.HashCode * 33
            unchecked
            {
                return (dstHashCode << 5) + dstHashCode;
            }
        }

        /// <summary>
        /// Generates a djb2 hash of destinationType without initial seed.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComputeHash(Mapping row) => ComputeHash(row.DestinationType, row.SourceType);
    }
}