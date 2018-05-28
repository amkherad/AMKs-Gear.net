using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Modeling.Annotations;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Modeling
{
    public static class ModelingHelper
    {
        #region GetPropertyInfos
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
        #endregion
        #region GetFieldInfos
        private static IEnumerable<FieldInfo> GetFieldInfos(Type type, Func<FieldInfo, bool> selector = null)
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
        private static IEnumerable<FieldInfo> GetFieldInfos(TypeInfo type, Func<FieldInfo, bool> selector = null)
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
        #endregion
        #region GetMembers
        public static IEnumerable<IModelMemberInfo> GetMembers(Type type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));
            members.AddRange(GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        public static IEnumerable<IModelMemberInfo> GetMembers(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null, Func<FieldInfo, bool> fieldSelector = null)
        {
            var members = new ModelMemberInfoCollection(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));
            members.AddRange(GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        #endregion

        #region GetProperties
        public static IEnumerable<IModelMemberInfo> GetProperties(Type type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        public static IEnumerable<IModelMemberInfo> GetProperties(TypeInfo type,
            Func<PropertyInfo, bool> propertySelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection(
                GetPropertyInfos(type, propertySelector).Select(x => new ModelPropertyInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        #endregion

        #region GetFields
        public static IEnumerable<IModelMemberInfo> GetFields(Type type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection(
                GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        public static IEnumerable<IModelMemberInfo> GetFields(TypeInfo type,
            Func<FieldInfo, bool> fieldSelector = null)
        {
            //var typeInfo = type.GetTypeInfo();
            var members = new ModelMemberInfoCollection(
                GetFieldInfos(type, fieldSelector).Select(x => new ModelFieldInfo(x)));

            var result = OrderMemberInfos(members);

            return result;
        }
        #endregion

        #region Helpers
        public static IEnumerable<IModelMemberInfo> OrderMemberInfos(IEnumerable<IModelMemberInfo> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var mustOrder = new List<Tuple<IModelMemberInfo, ModelMemberOrderAttribute>>();
            var result = new ModelMemberInfoCollection();

            foreach (var element in collection)
            {
                var attr = element.GetCustomAttributes(typeof(ModelMemberOrderAttribute), true);
                if (attr != null && attr.Any())
                    mustOrder.Add(new Tuple<IModelMemberInfo, ModelMemberOrderAttribute>
                        (element, attr.First() as ModelMemberOrderAttribute));
                else
                    result.Add(element);
            }

            var insertAfters = new List<Tuple<IModelMemberInfo, ModelMemberOrderAttribute>>();
            foreach (var order in mustOrder)
            {
                var member = order.Item1;
                var attr = order.Item2;
                if (attr is ModelMemberOrderInsertBeforeAttribute)
                {
                    insertAfters.Add(order);
                    continue;
                }

                var index = attr.Order;
                if (index == -1)
                    result.Add(member);
                else
                    result.Insert(index, member);
            }

            bool oneInserted = true;
            while (oneInserted && insertAfters.Any())
            {
                oneInserted = false;
                var inserts = insertAfters.ToArray();
                foreach (var order in inserts)
                {
                    var member = order.Item1;
                    var attr = order.Item2 as ModelMemberOrderInsertBeforeAttribute;
                    if (attr == null) continue;
                    var index = result.SequentialIndexOf(x => attr.NameComparer.Equals(x.Name, attr.MemberName));
                    if (index >= 0)
                    {
                        insertAfters.Remove(order);
                        result.Insert(index, member);
                        oneInserted = true;
                    }
                }
            }
            if (insertAfters.Any())
                result.AddRange(insertAfters.Select(x => x.Item1));

            return result;
        }
        #endregion
    }
}