//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class DoubleValidationAttribute : ValidationAttribute
//    {
//        public double Minimum { get; set; }
//        public double Maximum { get; set; }

//        public DoubleValidationAttribute() { Minimum = double.MinValue; Maximum = double.MaxValue; RequiredMode = new RequiredModes(); }
//        public DoubleValidationAttribute(bool nullable) { Minimum = double.MinValue; Maximum = double.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DoubleValidationAttribute(double minimum, bool nullable) { Minimum = minimum; Maximum = double.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public DoubleValidationAttribute(double minimum, double maximum, bool nullable) { Minimum = minimum; Maximum = maximum; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                DoubleDataValidator sdv = new DoubleDataValidator(Minimum, Maximum);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
