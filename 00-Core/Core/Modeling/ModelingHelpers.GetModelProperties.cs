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
        public static IEnumerable<IModelMemberInfo> GetCompatibleModelProperties(Type type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetCompatiblePropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        
        public static IEnumerable<IModelMemberInfo> GetCompatibleModelProperties(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection<IModelMemberInfo>(
                GetCompatiblePropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IModelValueMemberInfo> GetModelProperties(IEnumerable<PropertyInfo> propertyInfos)
            => propertyInfos.Select(fi => new ModelPropertyInfo(fi));
    }
}