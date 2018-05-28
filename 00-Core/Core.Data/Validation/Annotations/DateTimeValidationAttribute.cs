//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class DateTimeValidationAttribute : ValidationAttribute
//    {
//        public DateTime MinValue { get; set; }
//        public DateTime MaxValue { get; set; }

//        public DateTimeValidationAttribute() { MinValue = DateTime.MinValue; MaxValue = DateTime.MaxValue; RequiredMode = new RequiredModes(); }
//        public DateTimeValidationAttribute(bool nullable) { MinValue = DateTime.MinValue; MaxValue = DateTime.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DateTimeValidationAttribute(DateTime maxValue, bool nullable) { MinValue = DateTime.MinValue; MaxValue = maxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DateTimeValidationAttribute(DateTime minValue, DateTime maxValue, bool nullable) { MinValue = minValue; MaxValue = maxValue; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                StringDateTimeDataValidator sdv = new StringDateTimeDataValidator(MinValue, MaxValue);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
