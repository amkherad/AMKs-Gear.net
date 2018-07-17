using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    /// <summary>
    /// Provides abstract access to field info.
    /// </summary>
    public class ModelFieldInfo : ModelMemberInfo, IModelValueMemberInfo
    {
        public FieldInfo FieldInfo { get; protected set; }

        public ModelFieldInfo(/*Type type, TypeInfo typeInfo,*/ FieldInfo fieldInfo)
            : base(/*type, typeInfo,*/ fieldInfo)
        {
            FieldInfo = fieldInfo;
        }

        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public object GetValue(object instance) => FieldInfo.GetValue(instance);
        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public object GetValue(object instance, object defaultValue) => FieldInfo.GetValue(instance) ?? defaultValue;
        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public void SetValue(object instance, object value) => FieldInfo.SetValue(instance, value);
        
        
        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public Expression CreateGetExpression(Expression expression)
        {
            return Expression.Field(expression, FieldInfo);
        }

        /// <inheritdoc cref="IModelValueMemberInfo"/>
        public Expression CreateSetExpression(Expression expression, Expression value)
        {
            return Expression.Assign(Expression.Field(expression, FieldInfo), value);
        }

        public static implicit operator FieldInfo(ModelFieldInfo info) => info.FieldInfo;
        public static implicit operator ModelFieldInfo(FieldInfo info) => new ModelFieldInfo(info);
    }
}