using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        public static IEnumerable<PropertyInfo> GetPropertyInfos(Type type, Func<PropertyInfo, bool> selector = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            var props = selector == null
                ? type.GetRuntimeProperties()
                : type.GetRuntimeProperties().Where(selector);

            var typeInfo = type.GetTypeInfo();

            typeInfo.GetCustomAttribute(typeof(ModelSelfDescribedMembersAttribute), true);
            if (typeInfo.IsDefined(typeof(ModelSelfDescribedMembersAttribute), true))
            {
                return props.Where(x => x.IsDefined(typeof(ModelIncludeAttribute), true));
            }

            return props.Where(x => !x.IsDefined(typeof(ModelExcludeAttribute), true));
        }
        
        private static IEnumerable<PropertyInfo> GetPropertyInfos(TypeInfo type, Func<PropertyInfo, bool> selector = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var props = selector == null
                ? type.AsType().GetRuntimeProperties()
                : type.AsType().GetRuntimeProperties().Where(selector);

            type.GetCustomAttribute(typeof(ModelSelfDescribedMembersAttribute), true);
            if (type.IsDefined(typeof(ModelSelfDescribedMembersAttribute), true))
            {
                return props.Where(x => x.IsDefined(typeof(ModelIncludeAttribute), true));
            }

            return props.Where(x => !x.IsDefined(typeof(ModelExcludeAttribute), true));
        }
    }
}