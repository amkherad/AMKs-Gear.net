using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Data;

namespace AMKsGear.Core.Modeling
{
    public class ModelingCacheContext : CacheContext<Type, IModelMemberInfo[]>
    {
        public ModelingCacheContext()
        {
        }

        public ModelingCacheContext(int capacity)
            : base(capacity)
        {
        }
        
        public ModelingCacheContext(IDictionary<Type, IModelMemberInfo[]> dictionary)
            : base(dictionary)
        {
        }
        
        public ModelingCacheContext(IDictionary<Type, IModelMemberInfo[]> dictionary,
            IEqualityComparer<Type> comparer)
            : base(dictionary, comparer)
        {
        }

        

        public virtual IEnumerable<IModelValueMemberInfo> GetValueMembers(Type type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
            => TryGet(type, out var infos)
                ? infos.OfType<IModelValueMemberInfo>()
                : ModelingHelpers.GetCompatibleModelValueMembers(type, propertySelector, fieldSelector);

        public virtual IEnumerable<IModelValueMemberInfo> GetValueMembers(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
            => TryGet(type.AsType(), out var infos)
                ? infos.OfType<IModelValueMemberInfo>()
                : ModelingHelpers.GetCompatibleModelValueMembers(type, propertySelector, fieldSelector);

        public virtual IEnumerable<IModelMemberInfo> GetProperties(Type type,
            Func<PropertyInfo, bool> propertySelector = null)
            => TryGet(type, out var infos)
                ? infos
                : ModelingHelpers.GetCompatibleModelProperties(type, propertySelector);

        public virtual IEnumerable<IModelMemberInfo> GetProperties(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null)
            => TryGet(type.AsType(), out var infos)
                ? infos
                : ModelingHelpers.GetCompatibleModelProperties(type, propertySelector);

        public virtual IEnumerable<IModelMemberInfo> GetFields(Type type,
            Func<FieldInfo, bool> fieldSelector = null)
            => TryGet(type, out var infos)
                ? infos
                : ModelingHelpers.GetCompatibleModelFields(type, fieldSelector);

        public virtual IEnumerable<IModelMemberInfo> GetFields(TypeInfo type,
            Func<FieldInfo, bool> fieldSelector = null)
            => TryGet(type.AsType(), out var infos)
                ? infos
                : ModelingHelpers.GetCompatibleModelFields(type, fieldSelector);
    }
}