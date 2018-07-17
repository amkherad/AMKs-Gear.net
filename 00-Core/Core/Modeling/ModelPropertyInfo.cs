using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    /// <summary>
    /// Provides abstract access to property info.
    /// </summary>
    public class ModelPropertyInfo : ModelMemberInfo, IModelValueMemberInfo
    {
        public PropertyInfo PropertyInfo { get; protected set; }
        
        public ModelPropertyInfo( /*Type type, TypeInfo typeInfo,*/ PropertyInfo propertyInfo)
            : base( /*type, typeInfo,*/ propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public object GetValue(object instance) => PropertyInfo.GetValue(instance);
        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public object GetValue(object instance, object defaultValue) => PropertyInfo.GetValue(instance) ?? defaultValue;
        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public void SetValue(object instance, object value) => PropertyInfo.SetValue(instance, value);

        public void SetValue(object instance, object value, object[] index) =>
            PropertyInfo.SetValue(instance, value, index);

        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public Expression CreateGetExpression(Expression expression)
        {
            return Expression.Property(expression, PropertyInfo);
        }

        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public Expression CreateSetExpression(Expression expression, Expression value)
        {
            return Expression.Assign(Expression.Property(expression, PropertyInfo), value);
        }
        
        public static implicit operator PropertyInfo(ModelPropertyInfo info) => info.PropertyInfo;
        public static implicit operator ModelPropertyInfo(PropertyInfo info) => new ModelPropertyInfo(info);

    }
}