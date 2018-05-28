using System;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Modeling
{
    public class ModelPropertyInfo : ModelMemberInfo
    {
        protected readonly PropertyInfo PropertyInfo;

        public ModelPropertyInfo(/*Type type, TypeInfo typeInfo,*/ PropertyInfo propertyInfo)
            : base(/*type, typeInfo,*/ propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        public override object GetValue(object instance) => PropertyInfo.GetValue(instance);
        public override object GetValue(object instance, object defaultValue) => PropertyInfo.GetValue(instance) ?? defaultValue;
        public override void SetValue(object instance, object value) => PropertyInfo.SetValue(instance, value);
        public void SetValue(object instance, object value, object[] index) => PropertyInfo.SetValue(instance, value, index);

        public static implicit operator PropertyInfo(ModelPropertyInfo info) => info.PropertyInfo;
        public static implicit operator ModelPropertyInfo(PropertyInfo info) => new ModelPropertyInfo(info);

        public static object GetPropertyValue<T>(object obj, Expression<Func<T>> property)
            => GetPropertyValue(obj, property.GetMethodName());
        public static object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            var propertyInfo = obj.GetType().GetRuntimeProperty(propertyName);
            if (propertyInfo == null) throw new InvalidOperationException();
            return propertyInfo.GetValue(obj);
        }
    }
}