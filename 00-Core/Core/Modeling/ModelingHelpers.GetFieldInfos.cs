using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Modeling
{
    public static partial class ModelingHelpers
    {
        private static IEnumerable<FieldInfo> GetCompatibleFieldInfos(Type type, Func<FieldInfo, bool> selector = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            var fields = selector == null
                ? type.GetRuntimeFields()
                : type.GetRuntimeFields().Where(selector);

            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsDefined(typeof(ModelSelfDescribedMembersAttribute), true))
            {
                return fields.Where(x => x.IsDefined(typeof(ModelIncludeAttribute), true));
            }

            return fields.Where(x => !x.IsDefined(typeof(ModelExcludeAttribute), true));
        }
        
        private static IEnumerable<FieldInfo> GetCompatibleFieldInfos(TypeInfo type, Func<FieldInfo, bool> selector = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            var fields = selector == null
                ? type.AsType().GetRuntimeFields()
                : type.AsType().GetRuntimeFields().Where(selector);

            if (type.IsDefined(typeof(ModelSelfDescribedMembersAttribute), true))
            {
                return fields.Where(x => x.IsDefined(typeof(ModelIncludeAttribute), true));
            }

            return fields.Where(x => !x.IsDefined(typeof(ModelExcludeAttribute), true));
        }
    }
}