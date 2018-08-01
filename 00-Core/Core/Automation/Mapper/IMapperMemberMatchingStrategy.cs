using System.Collections.Generic;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Automation.Mapper
{
    public interface IMapperMemberMatchingStrategy
    {
        ICacheContext<string, IModelValueMemberInfo> ProcessSourceMemberNames(
            IDictionary<string, IModelValueMemberInfo> sourceMemberInfos
            );
        
        bool TryFindMatchingMember(
            string destinationName,
            IModelValueMemberInfo destinationMemberInfo,
            ICacheContext<string, IModelValueMemberInfo> sourceMemberInfos,
            out string sourceMemberName,
            out IModelValueMemberInfo sourceMemberInfo
        );
    }
}