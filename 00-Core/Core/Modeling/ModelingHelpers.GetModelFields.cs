using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        public static IEnumerable<IModelMemberInfo> GetCompatibleModelFields(Type type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetCompatibleFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        
        public static IEnumerable<IModelMemberInfo> GetCompatibleModelFields(TypeInfo type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetCompatibleFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IModelValueMemberInfo> GetModelFields(IEnumerable<FieldInfo> fieldInfos)
            => fieldInfos.Select(fi => new ModelFieldInfo(fi));
    }
}