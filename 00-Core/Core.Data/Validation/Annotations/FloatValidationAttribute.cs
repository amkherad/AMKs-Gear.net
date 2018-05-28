//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class FloatValidationAttribute : ValidationAttribute
//    {
//        public float Minimum { get; set; }
//        public float Maximum { get; set; }

//        public FloatValidationAttribute() { Minimum = float.MinValue; Maximum = float.MaxValue; RequiredMode = new RequiredModes(); }
//        public FloatValidationAttribute(bool nullable) { Minimum = float.MinValue; Maximum = float.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public FloatValidationAttribute(float minimum, bool nullable) { Minimum = minimum; Maximum = float.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public FloatValidationAttribute(float minimum, float maximum, bool nullable) { Minimum = minimum; Maximum = maximum; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                FloatDataValidator sdv = new FloatDataValidator(Minimum, Maximum);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
