using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        public static IEnumerable<IModelMemberInfo> GetProperties(Type type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        
        public static IEnumerable<IModelMemberInfo> GetProperties(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
    }
}