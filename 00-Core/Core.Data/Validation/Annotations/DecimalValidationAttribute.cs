//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class DecimalValidationAttribute : ValidationAttribute
//    {
//        public decimal Minimum { get; set; }
//        public decimal Maximum { get; set; }

//        public DecimalValidationAttribute() { Minimum = decimal.MinValue; Maximum = decimal.MaxValue; RequiredMode = new RequiredModes(); }
//        public DecimalValidationAttribute(bool nullable) { Minimum = decimal.MinValue; Maximum = decimal.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DecimalValidationAttribute(decimal minimum, bool nullable) { Minimum = minimum; Maximum = decimal.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DecimalValidationAttribute(decimal minimum, decimal maximum, bool nullable) { Minimum = minimum; Maximum = maximum; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                DecimalDataValidator sdv = new DecimalDataValidator(Minimum, Maximum);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
