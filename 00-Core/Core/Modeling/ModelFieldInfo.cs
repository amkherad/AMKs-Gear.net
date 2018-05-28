using System.Reflection;

namespace AMKsGear.Core.Modeling
{
    public class ModelFieldInfo : ModelMemberInfo
    {
        protected readonly FieldInfo FieldInfo;

        public ModelFieldInfo(/*Type type, TypeInfo typeInfo,*/ FieldInfo fieldInfo)
            : base(/*type, typeInfo,*/ fieldInfo)
        {
            FieldInfo = fieldInfo;
        }

        public override object GetValue(object instance) => FieldInfo.GetValue(instance);
        public override object GetValue(object instance, object defaultValue) => FieldInfo.GetValue(instance) ?? defaultValue;
        public override void SetValue(object instance, object value) => FieldInfo.SetValue(instance, value);

        public static implicit operator FieldInfo(ModelFieldInfo info) => info.FieldInfo;
        public static implicit operator ModelFieldInfo(FieldInfo info) => new ModelFieldInfo(info);
    }
}