using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Automation.Reflection;
using AMKsGear.Core.Linq.Convert;
using AMKsGear.Core.Modeling;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MapperHelpers
    {
        public static List<Mapping.MemberMapInfo> GetMemberMappings(
            Type destinationType,
            Type sourceType,
            BindingFlags destinationBindingFlags,
            BindingFlags sourceBindingFlags,
            IMapperMemberMatchingStrategy mapperMemberMatchingStrategy,
            IEnumerable<IModelValueMemberFilter> destinationFilters,
            IEnumerable<IModelValueMemberFilter> sourceFilters,
            MappingType mappingType
        )
        {
            if (destinationType == null) throw new ArgumentNullException(nameof(destinationType));
            if (sourceType == null) throw new ArgumentNullException(nameof(sourceType));
            if (mapperMemberMatchingStrategy == null)
                throw new ArgumentNullException(nameof(mapperMemberMatchingStrategy));

            var result = new List<Mapping.MemberMapInfo>();

            IDictionary<string, IModelValueMemberInfo> destinationMembers =
                ModelingHelpers.GetModelFields(destinationType, destinationBindingFlags | BindingFlags.SetField)
                    .Union(
                        ModelingHelpers.GetModelProperties(destinationType,
                            destinationBindingFlags | BindingFlags.SetProperty)
                    )
                    .ToDictionary(key => key.Name);

            IDictionary<string, IModelValueMemberInfo> sourceMembers =
                ModelingHelpers.GetModelFields(destinationType, sourceBindingFlags | BindingFlags.GetField)
                    .Union(
                        ModelingHelpers.GetModelProperties(destinationType,
                            sourceBindingFlags | BindingFlags.GetProperty)
                    )
                    .ToDictionary(key => key.Name);

            if (destinationFilters != null)
            {
                foreach (var filter in destinationFilters)
                {
                    destinationMembers = filter.Filter(destinationMembers);
                }
            }

            if (sourceFilters != null)
            {
                foreach (var filter in sourceFilters)
                {
                    sourceMembers = filter.Filter(sourceMembers);
                }
            }

            var convertedSourceMembers = mapperMemberMatchingStrategy.ProcessSourceMemberNames(sourceMembers);

            foreach (var destMember in destinationMembers)
            {
                var destName = destMember.Key;
                var destInfo = destMember.Value;

                if (mapperMemberMatchingStrategy.TryFindMatchingMember(
                    destName,
                    destInfo,
                    convertedSourceMembers,
                    out var sourceMemberName,
                    out var sourceMemberInfo
                ))
                {
                    result.Add(GetSourceToDestinationMap(
                        destName,
                        destInfo,
                        sourceMemberName,
                        sourceMemberInfo
                    ));
                }
                else
                {
                    //TODO: match not found.
                }
            }

            //mappingStrategy = MappingStrategy.ShouldInline;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mapping.MemberMapInfo GetSourceToDestinationMap(
            string destinationName,
            IModelValueMemberInfo destinationMemberInfo,
            string sourceName,
            IModelValueMemberInfo sourceMemberInfo
        )
        {
            var destType = destinationMemberInfo.Type;
            var srcType = sourceMemberInfo.Type;

            if (destType.IsPrimitiveOrDecimal())
            {
                if (srcType.IsPrimitiveOrDecimal() ||
                    srcType == typeof(string))
                {
                    return new Mapping.MemberMapInfo
                    {
                        DestinationMember = destinationMemberInfo,
                        SourceMember = sourceMemberInfo,
                        TypeConverter = PrimitiveConvertHelper.Instance,
                        MemberMapType = Mapping.MemberMapType.PrimitiveToPrimitive
                    };
                }
                else
                {
                }
            }
            else if (destType == typeof(string))
            {
                return new Mapping.MemberMapInfo
                {
                    DestinationMember = destinationMemberInfo,
                    SourceMember = sourceMemberInfo,
                    TypeConverter = StringConvertHelper.Instance,
                    MemberMapType = Mapping.MemberMapType.PrimitiveToPrimitive
                };
            }
            else
            {
            }

            return null;
        }
    }
}