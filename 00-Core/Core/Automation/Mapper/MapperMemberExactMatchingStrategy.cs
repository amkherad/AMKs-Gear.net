using System.Collections.Generic;
using System.Threading;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Data;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MapperMemberExactMatchingStrategy : IMapperMemberMatchingStrategy
    {
        private static MapperMemberExactMatchingStrategy _instance;

        public static MapperMemberExactMatchingStrategy Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                return LazyInitializer.EnsureInitialized(ref _instance);
            }
        }


        public ICacheContext<string, IModelValueMemberInfo> ProcessSourceMemberNames(IDictionary<string, IModelValueMemberInfo> sourceMemberInfos)
        {
            return CacheContext<string, IModelValueMemberInfo>.Adapt(sourceMemberInfos.AsDictionary());
        }

        public bool TryFindMatchingMember(
            string destinationName,
            IModelValueMemberInfo destinationMemberInfo,
            ICacheContext<string, IModelValueMemberInfo> sourceMemberInfos,
            out string sourceMemberName,
            out IModelValueMemberInfo sourceMemberInfo)
        {
            if (sourceMemberInfos.TryGetValue(destinationName, out sourceMemberInfo))
            {
                sourceMemberName = sourceMemberInfo.Name;
                return true;
            }

            sourceMemberName = default;
            sourceMemberInfo = default;
            return false;
        }
    }
}