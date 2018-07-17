using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        public static IEnumerable<IModelValueMemberInfo> GetValueMembers(Type type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelValueMemberInfo>(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));
            members.AddRange(GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        
        public static IEnumerable<IModelValueMemberInfo> GetValueMembers(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            var members = new ModelMemberInfoCollection<IModelValueMemberInfo>(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));
            members.AddRange(GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
    }
}