//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class Int64ValidationAttribute : ValidationAttribute
//    {
//        public long Minimum { get; set; }
//        public long Maximum { get; set; }

//        public Int64ValidationAttribute() { Minimum = long.MinValue; Maximum = long.MaxValue; RequiredMode = new RequiredModes(); }
//        public Int64ValidationAttribute(bool nullable) { Minimum = long.MinValue; Maximum = long.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public Int64ValidationAttribute(long minimum, bool nullable) { Minimum = minimum; Maximum = long.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public Int64ValidationAttribute(long minimum, long maximum, bool nullable) { Minimum = minimum; Maximum = maximum; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                Int64DataValidator sdv = new Int64DataValidator(Minimum, Maximum);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
