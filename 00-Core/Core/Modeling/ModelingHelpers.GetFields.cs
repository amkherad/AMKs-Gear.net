using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        public static IEnumerable<IModelMemberInfo> GetFields(Type type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        
        public static IEnumerable<IModelMemberInfo> GetFields(TypeInfo type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
    }
}