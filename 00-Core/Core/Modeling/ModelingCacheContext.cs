using System;
using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Automation;

namespace AMKsGear.Core.Modeling
{
    public class ModelingCacheContext : TypeCacheContext<IModelMemberInfo[]>
    {
        #region GetMembers
        public virtual IEnumerable<IModelMemberInfo> GetMembers(Type type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type, out infos)
                ? infos
                : ModelingHelper.GetMembers(type, propertySelector, fieldSelector);
        }
        public virtual IEnumerable<IModelMemberInfo> GetMembers(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type.AsType(), out infos)
                ? infos
                : ModelingHelper.GetMembers(type, propertySelector, fieldSelector);
        }
        #endregion

        #region GetProperties
        public virtual IEnumerable<IModelMemberInfo> GetProperties(Type type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type, out infos)
                ? infos
                : ModelingHelper.GetProperties(type, propertySelector);
        }
        public virtual IEnumerable<IModelMemberInfo> GetProperties(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type.AsType(), out infos)
                ? infos
                : ModelingHelper.GetProperties(type, propertySelector);
        }
        #endregion

        #region GetFields
        public virtual IEnumerable<IModelMemberInfo> GetFields(Type type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type, out infos)
                ? infos
                : ModelingHelper.GetFields(type, fieldSelector);
        }
        public virtual IEnumerable<IModelMemberInfo> GetFields(TypeInfo type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            IModelMemberInfo[] infos;
            return GetState(type.AsType(), out infos)
                ? infos
                : ModelingHelper.GetFields(type, fieldSelector);
        }
        #endregion
    }
}