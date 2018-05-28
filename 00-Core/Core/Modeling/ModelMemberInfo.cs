using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public abstract class ModelMemberInfo : IModelMemberInfo
    {
        //protected readonly Type TargetType;
        //protected readonly TypeInfo TargetTypeInfo;
        protected readonly MemberInfo MemberInfo;
        protected readonly Type MemberType;

        public ModelMemberInfo(/*Type type, TypeInfo typeInfo,*/ MemberInfo memberInfo, Type memberType)
        {
            //TargetType = type;
            //TargetTypeInfo = typeInfo;
            MemberInfo = memberInfo;
        }
        public ModelMemberInfo(/*Type type, TypeInfo typeInfo,*/ PropertyInfo propertyInfo)
        {
            //TargetType = type;
            //TargetTypeInfo = typeInfo;
            MemberInfo = propertyInfo;
            MemberType = propertyInfo.PropertyType;
        }
        public ModelMemberInfo(/*Type type, TypeInfo typeInfo,*/ FieldInfo fieldInfo)
        {
            //TargetType = type;
            //TargetTypeInfo = typeInfo;
            MemberInfo = fieldInfo;
            MemberType = fieldInfo.FieldType;
        }

        public object GetUnderlyingContext() => MemberInfo;

        #region Extended Members

        #endregion

        #region MemberInfo Members
        public virtual IEnumerable<CustomAttributeData> CustomAttributes => MemberInfo.CustomAttributes;
        //public Type DeclaringType => MemberInfo.DeclaringType;
        //public virtual Module Module => MemberInfo.Module;
        public string Name => MemberInfo.Name;
        public Type Type => MemberType;
        //public Type ReflectedType => TargetTypeInfo.;

        public IEnumerable<Attribute> GetCustomAttributes(bool inherit) => MemberInfo.GetCustomAttributes(inherit).Cast<Attribute>();
        public IEnumerable<Attribute> GetCustomAttributes(Type attributeType, bool inherit) => MemberInfo.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();


        public bool IsDefined(Type attributeType, bool inherit) => MemberInfo.IsDefined(attributeType, inherit);

        public abstract object GetValue(object instance);
        public abstract object GetValue(object instance, object defaultValue);

        public abstract void SetValue(object instance, object value);

        #endregion

        #region Equality Check Members
        public override int GetHashCode() => MemberInfo?.GetHashCode() ?? 0;

        protected bool Equals(ModelMemberInfo other) => Equals(MemberInfo, other.MemberInfo);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ModelMemberInfo)obj);
        }
        public static bool operator ==(ModelMemberInfo left, ModelMemberInfo right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (((object)left == null) || ((object)right == null)) return false;
            return Equals(left, right);
        }
        public static bool operator !=(ModelMemberInfo left, ModelMemberInfo right) => !(left == right);
        #endregion


        public static implicit operator MemberInfo(ModelMemberInfo info) => info.MemberInfo;
        //public static implicit operator ModelMemberInfo(MemberInfo info) => new ModelPropertyInfo(info);
        public static implicit operator ModelMemberInfo(PropertyInfo info) => new ModelPropertyInfo(info);
        public static implicit operator ModelMemberInfo(FieldInfo info) => new ModelFieldInfo(info);
    }
}
